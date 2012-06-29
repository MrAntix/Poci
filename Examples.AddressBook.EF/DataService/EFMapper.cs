namespace Examples.AddressBook.EF.DataService
{
    public interface IMapper<in TI, T>
    {
        T Map(TI i, T c = default(T));
    }
}