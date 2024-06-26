using Hangfire;

namespace Yoma.Core.Api.Middleware
{
  public class HangfireActivator : JobActivator
  {
    readonly IServiceScopeFactory _serviceScopeFactory;

    public HangfireActivator(IServiceScopeFactory serviceScopeFactory)
    {
      ArgumentNullException.ThrowIfNull(serviceScopeFactory, nameof(serviceScopeFactory));

      _serviceScopeFactory = serviceScopeFactory;
    }

    public override JobActivatorScope BeginScope(JobActivatorContext context)
    {
      return new HangfireActivatorScope(_serviceScopeFactory.CreateScope());
    }
  }
}
