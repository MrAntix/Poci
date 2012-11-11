using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Testing
{
    public static class Extensions
    {
        public static T OneOf<T>(this IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException("items");

            return OneOfNoCheck(items.ToArray());
        }

        static T OneOfNoCheck<T>(T[] itemsArray)
        {
            return itemsArray[TestData.Random.Value.Next(0, itemsArray.Length)];
        }

        public static IEnumerable<T> ManyOf<T>(
            this IEnumerable<T> items, int exactCount)
        {
            if (items == null) throw new ArgumentNullException("items");
            var itemsArray = items.ToArray();

            return Enumerable.Range(0, exactCount)
                .Select(i => OneOfNoCheck(itemsArray));
        }

        public static IEnumerable<T> ManyOf<T>(
            this IEnumerable<T> items, int minCount, int maxCount)
        {
            if (items == null) throw new ArgumentNullException("items");
            var itemsArray = items.ToArray();

            return Enumerable
                .Range(0, TestData.Random.Value.Next(minCount, maxCount))
                .Select(i => OneOfNoCheck(itemsArray));
        }

        public static ManifestResourceInfo FindResourceInfo(this Type type, string name)
        {
            return (from r in type.Assembly.GetManifestResourceNames()
                    where r.EndsWith("." + name)
                    select type.Assembly.GetManifestResourceInfo(r)).FirstOrDefault();
        }

        public static Stream FindResourceStream(this Type type, string name)
        {
            return (from r in type.Assembly.GetManifestResourceNames()
                    where r.EndsWith("." + name)
                    select type.Assembly.GetManifestResourceStream(r)).FirstOrDefault();
        }

        public static T FindResource<T>(this Type type, string name, Func<Stream, T> transformer)
        {
            return transformer(
                FindResourceStream(type, name));
        }

        public static string FindResourceString(this Type type, string name)
        {
            return FindResource(type, name,
                                s =>
                                    {
                                        using (var reader = new StreamReader(s))
                                            return reader.ReadToEnd();
                                    });
        }

        public static Image FindResourceImage(this Type type, string name)
        {
            return FindResource(type, name, Image.FromStream);
        }
    }
}