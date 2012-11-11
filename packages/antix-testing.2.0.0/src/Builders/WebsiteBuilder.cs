using Testing.Abstraction;
using Testing.Abstraction.Base;
using Testing.Abstraction.Builders;
using Testing.Models;

namespace Testing.Builders
{
    public class WebsiteBuilder :
        BuilderBase<IWebsiteBuilder, WebsiteModel>,
        IWebsiteBuilder
    {
        readonly IDataContainer _dataContainer;

        public WebsiteBuilder(IDataContainer dataContainer)
        {
            _dataContainer = dataContainer;
            Assign = x =>
                         {
                             var subDomain = "";
                             switch ((int) _dataContainer.Double
                                               .WithRange(0, 10)
                                               .Build())
                             {
                                 case 1:
                                 case 2:
                                 case 3:
                                 case 4:
                                 case 5:
                                     subDomain = "www.";
                                     break;
                                 case 6:
                                     subDomain =
                                         _dataContainer.Text
                                             .WithRange(1, 10)
                                             .WithCharacters(_dataContainer.Resources.Letters)
                                             .Build()
                                         + ".";
                                     break;
                             }

                             x.Address = string.Format(
                                 "http://{0}{1}/",
                                 subDomain,
                                 _dataContainer.Resources.WebDomains.OneOf()
                                 );
                         };
        }

        protected override IWebsiteBuilder CreateClone()
        {
            return new WebsiteBuilder(_dataContainer);
        }
    }
}