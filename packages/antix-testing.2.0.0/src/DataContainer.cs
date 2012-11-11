using Testing.Abstraction;
using Testing.Abstraction.Builders;
using Testing.Builders;

namespace Testing
{
    public class DataContainer :
        IDataContainer
    {
        IDataResources _resources;

        EmailBuilder _email;
        PersonBuilder _person;
        WebsiteBuilder _website;
        IBooleanBuilder _boolean;
        IDoubleBuilder _double;
        IIntegerBuilder _integer;
        IDateTimeBuilder _dateTime;
        ITextBuilder _text;

        public DataContainer(IDataResources resources)
        {
            _resources = resources;
        }

        #region IDataContainer Members

        public IDataResources Resources
        {
            get { return _resources; }
        }

        public virtual IBooleanBuilder Boolean
        {
            get
            {
                return _boolean
                       ?? (_boolean = new BooleanBuilder());
            }
        }

        public virtual IIntegerBuilder Integer
        {
            get
            {
                return _integer
                       ?? (_integer = new IntegerBuilder());
            }
        }

        public virtual IDoubleBuilder Double
        {
            get
            {
                return _double
                       ?? (_double = new DoubleBuilder());
            }
        }

        public virtual IDateTimeBuilder DateTime
        {
            get
            {
                return _dateTime
                       ?? (_dateTime = new DateTimeBuilder());
            }
        }

        public virtual ITextBuilder Text
        {
            get
            {
                return _text
                       ?? (_text = new TextBuilder(_resources));
            }
        }

        public virtual IPersonBuilder Person
        {
            get
            {
                return _person
                       ?? (_person = new PersonBuilder(this));
            }
        }

        public virtual IEmailBuilder Email
        {
            get
            {
                return _email
                       ?? (_email = new EmailBuilder(this));
            }
        }

        public virtual IWebsiteBuilder Website
        {
            get
            {
                return _website
                       ?? (_website = new WebsiteBuilder(this));
            }
        }

        #endregion
    }
}