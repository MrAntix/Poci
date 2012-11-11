using System;
using System.Threading;
using Testing.Abstraction;
using Testing.Abstraction.Builders;

namespace Testing
{
    public sealed class TestData
    {
        static int _randomSeed = Environment.TickCount;

        internal static readonly ThreadLocal<Random> Random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _randomSeed)));

        internal static readonly ThreadLocal<IDataContainer> Container =
            new ThreadLocal<IDataContainer>(() => new DataContainer(new DataResources()));

        public static IDataResources Resources
        {
            get { return Container.Value.Resources; }
        }

        public static IBooleanBuilder Boolean
        {
            get { return Container.Value.Boolean; }
        }

        public static IIntegerBuilder Integer
        {
            get { return Container.Value.Integer; }
        }

        public static IDoubleBuilder Double
        {
            get { return Container.Value.Double; }
        }

        public static IDateTimeBuilder DateTime
        {
            get { return Container.Value.DateTime; }
        }

        public static ITextBuilder Text
        {
            get { return Container.Value.Text; }
        }

        public static IPersonBuilder Person
        {
            get { return Container.Value.Person; }
        }

        public static IEmailBuilder Email
        {
            get { return Container.Value.Email; }
        }

        public static IWebsiteBuilder Website
        {
            get { return Container.Value.Website; }
        }
    }
}