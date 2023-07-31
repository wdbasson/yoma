using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Transactions;
using Yoma.Core.Domain.Core.Helpers;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Core.Validators;
using Yoma.Core.Domain.Exceptions;

namespace Yoma.Core.Domain.Core.Services
{
    public class S3ObjectService : IS3ObjectService
    {
        #region Class Variables
        private readonly IEnvironmentProvider _environmentProvider;
        private readonly IAmazonS3 _s3Client;
        private readonly AWSSettings _aWSOptions;
        private readonly IRepository<Models.S3Object> _s3ObjectRepository;
        #endregion

        #region Constructor
        public S3ObjectService(IEnvironmentProvider environmentProvider, IAmazonS3 s3Client, IOptions<AWSSettings> aWSOptions, IRepository<Models.S3Object> s3ObjectRepository)
        {
            _environmentProvider = environmentProvider;
            _s3Client = s3Client;
            _aWSOptions = aWSOptions.Value;
            _s3ObjectRepository = s3ObjectRepository;
        }
        #endregion

        #region Public Members
        public Models.S3Object GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var result = _s3ObjectRepository.Query().SingleOrDefault(o => o.Id == id);

            if (result == null)
                throw new ArgumentOutOfRangeException(nameof(id), $"S3Object with id '{id}' does not exist");

            return result;
        }

        public async Task<Models.S3Object> Create(IFormFile file, FileTypeEnum type)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            new FileValidator(type).Validate(file);

            var id = Guid.NewGuid();
            var key = $"{_environmentProvider.Environment}/{type}/{id}";

            var result = new Models.S3Object
            {
                Id = id,
                ObjectKey = key,
                DateCreated = DateTimeOffset.Now
            };

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                await _s3ObjectRepository.Create(result);

                using (var stream = new MemoryStream(file.ToBinary()))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = _aWSOptions.S3BucketName,
                        Key = key,
                        InputStream = stream,
                        ContentType = file.ContentType
                    };

                    try
                    {
                        await _s3Client.PutObjectAsync(request);
                    }
                    catch (AmazonS3Exception ex)
                    {
                        throw new TechnicalException($"Failed to upload S3 object with key '{key}'", ex);
                    }
                }

                scope.Complete();
            }

            return result;
        }

        public string GetURL(Guid id)
        {
            var item = GetById(id);

            var request = new GetPreSignedUrlRequest
            {
                BucketName = _aWSOptions.S3BucketName,
                Key = item.ObjectKey,
                Expires =  DateTime.UtcNow.AddMinutes(_aWSOptions.S3URLExpirationInMinutes) 
            };

            try
            {
                return _s3Client.GetPreSignedURL(request);
            }
            catch (AmazonS3Exception ex)
            {
                throw new TechnicalException($"Failed to retrieve URL for S3 object with key '{item.ObjectKey}'", ex);
            }
        }

        public async Task Delete(Guid id)
        {
            var item = GetById(id);

            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                await _s3ObjectRepository.Delete(item);

                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _aWSOptions.S3BucketName,
                    Key = item.ObjectKey
                };

                try
                {
                    await _s3Client.DeleteObjectAsync(deleteRequest);
                    
                }
                catch (AmazonS3Exception ex)
                {
                    throw new TechnicalException($"Failed to delete S3 object with key '{item.ObjectKey}'", ex);
                }

                scope.Complete();
            }
        }
        #endregion
    }
}
