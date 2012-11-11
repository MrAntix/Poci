using System;
using Testing.Abstraction;
using Testing.Abstraction.Base;
using Testing.Abstraction.Builders;
using Testing.Models;

namespace Testing.Builders
{
    public class EmailBuilder :
        BuilderBase<IEmailBuilder, EmailModel>,
        IEmailBuilder
    {
        readonly IDataContainer _dataContainer;
        PersonModel _person;

        public EmailBuilder(IDataContainer dataContainer)
        {
            _dataContainer = dataContainer;
            Assign = email =>
                         {
                             if (_person == null)
                                 throw new InvalidOperationException(
                                     "Person must be set to generate e-mails, use builder.WithPerson(person)");

                             email.Type = _dataContainer.Resources.EmailTypes.OneOf();
                             email.Address = string.Format("{0}.{1}@{2}",
                                                           _person.FirstName.Trim().ToLower(),
                                                           _person.LastName.Trim().ToLower(),
                                                           _dataContainer.Resources.WebDomains.OneOf());
                         };
        }

        public IEmailBuilder WithPerson(PersonModel person)
        {
            _person = person;

            return this;
        }

        protected override IEmailBuilder CreateClone()
        {
            return new EmailBuilder(_dataContainer);
        }
    }
}