using System;
using System.Collections.Generic;
using Testing.Abstraction;
using Testing.Abstraction.Base;
using Testing.Abstraction.Builders;
using Testing.Models;

namespace Testing.Builders
{
    public interface IPersonBuilderResources
    {
        IEnumerable<string> PersonFirstNamesMale { get; }
        IEnumerable<string> PersonFirstNamesFemale { get; }
        IEnumerable<GenderTypes> DataGenders { get; }
        IEnumerable<string> PersonLastNames { get; }

        IDateTimeBuilder DateTime { get; }
    }

    public class PersonBuilder :
        BuilderBase<IPersonBuilder, PersonModel>,
        IPersonBuilder

    {
        readonly IDataContainer _dataContainer;

        public PersonBuilder(IDataContainer dataContainer)
        {
            _dataContainer = dataContainer;
            Assign = p =>
                         {
                             p.Gender = _dataContainer.Resources.DataGenders.OneOf();
                             p.FirstName = p.Gender == GenderTypes.Female
                                               ? _dataContainer.Resources.PersonFirstNamesFemale.OneOf()
                                               : _dataContainer.Resources.PersonFirstNamesMale.OneOf();
                             p.LastName = _dataContainer.Resources.PersonLastNames.OneOf();

                             p.DateOfBirth = dataContainer.DateTime
                                 .WithRange(DateTime.UtcNow.AddYears(-130), DateTime.UtcNow)
                                 .Build();
                         };
        }

        public IPersonBuilder WithAge(int min, int max)
        {
            return With(
                p =>
                    {
                        p.DateOfBirth = _dataContainer.DateTime
                            .WithRange(DateTime.UtcNow.AddYears(-max), DateTime.UtcNow.AddYears(-min))
                            .Build();
                    });
        }

        public IPersonBuilder WithGender(GenderTypes gender)
        {
            return With(
                p => { p.Gender = gender; });
        }

        protected override IPersonBuilder CreateClone()
        {
            return new PersonBuilder(_dataContainer);
        }
    }
}