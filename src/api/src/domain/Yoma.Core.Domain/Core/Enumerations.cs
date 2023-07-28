using System.ComponentModel;

namespace Yoma.Core.Domain.Core
{
    public enum Environment
    {
        None,
        [Description("Local")]
        Local,
        [Description("Development")]
        Develop,
        [Description("Testing / Staging")]
        Stage,
        [Description("Production")]
        Prod
    }

    [Flags]
    public enum ReferenceDataType
    {
        None = 0,
        Lookups = 1 //lookup entities i.e. countries; reference data store in lookup db namespace
    }
}
