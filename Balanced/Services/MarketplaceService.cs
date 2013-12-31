using System;
using Balanced.Entities;

namespace Balanced.Services
{    
    public class MarketplaceService : BalancedServices<Marketplace>
    {

        public override string RootUri
        {
            get
            {

                return string.Format("/{0}/marketplaces", BalancedHttpRest.Version);
            }
        }

        public MarketplaceService(string secret) : base(secret)
        {
        }

        public new Marketplace Get(Marketplace marketplace)
        {
            return base.Get(marketplace);
        }

        public new PagedList<Marketplace> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }
    }
}