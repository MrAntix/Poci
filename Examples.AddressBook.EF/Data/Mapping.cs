using AutoMapper;
using Examples.AddressBook.Data;
using Poci.Contacts.Data;
using Poci.Security.Data;

namespace Examples.AddressBook.EF.Data
{
    public static class Mapping
    {
        static Mapping()
        {
            Mapper.CreateMap<IUser, EFUser>();
            Mapper.CreateMap<ISession, EFSession>();
            Mapper.CreateMap<IAddressBookContact, EFContact>();
            Mapper.CreateMap<IEmail, EFEmail>();
            Mapper.CreateMap<IPhone, EFPhone>();
            Mapper.CreateMap<IAddress, EFAddress>();
        }

        public static TC Map<TI, TC>(TI value)
            where TC : TI
        {
            return value is TC ? (TC) value : Mapper.Map<TI, TC>(value);
        }

        public static EFUser Map(this IUser value)
        {
            return Map<IUser, EFUser>(value);
        }

        public static EFSession Map(this ISession value)
        {
            return Map<ISession, EFSession>(value);
        }

        public static EFContact Map(this IAddressBookContact value)
        {
            return Map<IAddressBookContact, EFContact>(value);
        }

        public static EFEmail Map(this IEmail value)
        {
            return Map<IEmail, EFEmail>(value);
        }

        public static EFPhone Map(this IPhone value)
        {
            return Map<IPhone, EFPhone>(value);
        }

        public static EFAddress Map(this IAddress value)
        {
            return Map<IAddress, EFAddress>(value);
        }
    }
}