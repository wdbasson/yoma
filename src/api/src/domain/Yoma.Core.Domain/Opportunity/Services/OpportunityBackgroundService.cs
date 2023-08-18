using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Opportunity.Interfaces;
using Yoma.Core.Domain.Opportunity.Interfaces.Lookups;

namespace Yoma.Core.Domain.Opportunity.Services
{
    public class OpportunityBackgroundService : IOpportunityBackgroundService
    {
        #region Class Variables
        private readonly ScheduleJobOptions _scheduleJobOptions;
        private readonly IOpportunityStatusService _opportunityStatusService;
        private readonly IRepositoryValueContainsWithNavigation<Models.Opportunity> _opportunityRepository;
        private static readonly Status[] Statuses_Expirable = { Status.Active, Status.Inactive };
        #endregion

        #region Constructor
        public OpportunityBackgroundService(IOptions<ScheduleJobOptions> scheduleJobOptions, 
            IOpportunityStatusService opportunityStatusService,
            IRepositoryValueContainsWithNavigation<Models.Opportunity> opportunityRepository)
        {
            _scheduleJobOptions = scheduleJobOptions.Value;
            _opportunityStatusService = opportunityStatusService;   
            _opportunityRepository = opportunityRepository;
        }
        #endregion

        public async Task ProcessExpiration()
        {
            var statusExpiredId = _opportunityStatusService.GetByName(Status.Expired.ToString()).Id;
            var statusExpirableIds = Statuses_Expirable.Select(o => _opportunityStatusService.GetByName(o.ToString()).Id).ToList();

            do
            {
                var items = _opportunityRepository.Query().Where(o => statusExpirableIds.Contains(o.StatusId) &&
                    o.DateEnd.HasValue && o.DateEnd.Value <= DateTimeOffset.Now).OrderBy(o => o.DateEnd).Take(_scheduleJobOptions.OpportunityExpirationBatchSize).ToList();
                if (!items.Any()) break;

                foreach (var item in items)
                {
                    //TODO: email notification to provider

                    item.StatusId = statusExpiredId;
                    await _opportunityRepository.Update(item);
                }
            } while (true);
        }

        public async Task ExpirationNotifications()
        {
            var datetimeFrom = new DateTimeOffset(DateTime.Today);
            var datetimeTo = datetimeFrom.AddDays(_scheduleJobOptions.OpportunityExpirationNotificationIntervalInDays);
            var statusExpirableIds = Statuses_Expirable.Select(o => _opportunityStatusService.GetByName(o.ToString()).Id).ToList();

            do
            {
                var items = _opportunityRepository.Query().Where(o => statusExpirableIds.Contains(o.StatusId) &&
                    o.DateEnd.HasValue && o.DateEnd.Value >= datetimeFrom && o.DateEnd.Value <= datetimeTo).OrderBy(o => o.DateEnd).Take(_scheduleJobOptions.OpportunityExpirationBatchSize).ToList();
                if (!items.Any()) break;

                //TODO: email notification to provider

            } while (true);
        }
    }
}
