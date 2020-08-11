using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Deza.Stone.Utilities
{ /// <summary>
  /// This class provides to convert object to any POCO model
  /// </summary>
    public static class PocoUtils
    {
        /// <summary>
        /// Bu fonksiyon bir nesneyi başka bir nesneye dönüştürmeye yarar. Dönüştürülecek olan nesne içerisinde bulunan 
        /// propertylere göre hedef nesneye dönüştürülür. 
        /// Kaynak property adı ve hedef property adı aynı olmak zorundadır (Büyük küçük harf duyarlılığı vardır).
        /// Propertysi eşleşmeyen veriler dönüştürülmez
        /// </summary>
        /// <typeparam name="T">Dönüştürülmek istenen kaynak nesne tipidir. (Type)</typeparam>
        /// <typeparam name="TU">Dönüştürülecek olan hedef nesne tipidir. (Type)</typeparam>
        /// <param name="source">Dönüştürülecek olan nesnedir.(Source)</param>
        /// <returns>Hedef nesne tipinde dönüştürülen nesnedir.</returns>
        public static TU Convert<T, TU>(T source)
            where T : class, new()
            where TU : class, new()
        {
            var destinationItem = Convert<T, TU>(source, null);

            return destinationItem;
        }

        /// <summary>
        /// Bu fonksiyon bir nesneyi başka bir nesneye dönüştürmeye yarar. Dönüştürülecek olan nesne içerisinde bulunan 
        /// propertylere göre hedef nesneye dönüştürülür. 
        /// Kaynak property adı ve hedef property adı aynı olmak zorundadır (Büyük küçük harf duyarlılığı vardır).
        /// Propertysi eşleşmeyen veriler dönüştürülmez.
        /// </summary>
        /// <typeparam name="T">Dönüştürülmek istenen kaynak nesne tipidir. (Type)</typeparam>
        /// <typeparam name="TU">Dönüştürülecek olan hedef nesne tipidir. (Type)</typeparam>
        /// <param name="source">Dönüştürülecek olan nesnedir. (Source)</param>
        /// <param name="excludeList">Nesnenin içerisinde dönüştürülmesi istenmeyen propertyler varsa liste şeklinde verilir.</param>
        /// <returns>Hedef nesne tipinde dönüştürülen nesnedir.</returns>
        public static TU Convert<T, TU>(T source, IEnumerable<string> excludeList)
            where T : class, new()
            where TU : class, new()
        {
            var sourceProperties = source.GetType().GetProperties().ToList();

            var destinationItem = new TU();
            var destinationProperties = destinationItem.GetType().GetProperties().ToList();

            foreach (var sourceProperty in sourceProperties)
            {
                if (excludeList != null)
                {
                    if (excludeList.Contains(sourceProperty.Name)) //TODO ibrahim: contains'in string listlerde kullanılması bazen aranan kelimenin birden fazla itemda geçmesi durumunda yanlış kararlara sebep olabilmektdir, bu gibi durumlarda liste içerisinde Any(x=>x.prop == "...") ile kontrolü daha risksiz olacaktır
                    {
                        continue;
                    }
                }

                var destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    try
                    {
                        var propVal = sourceProperty.GetValue(source, null);
                        destinationProperty.SetValue(destinationItem, propVal, null);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }

            return destinationItem;
        }

        /// <summary>
        /// Bu fonksiyon bir nesneyi başka bir nesneye dönüştürmeye yarar. Dönüştürülecek olan nesne içerisinde bulunan 
        /// propertylere göre hedef nesneye dönüştürülür. 
        /// Kaynak property adı ve hedef property adı aynı olmak zorundadır (Büyük küçük harf duyarlılığı vardır).
        /// Propertysi eşleşmeyen veriler dönüştürülmez
        /// </summary>
        /// <typeparam name="T">Dönüştürülmek istenen kaynak nesne tipidir. (Type)</typeparam>
        /// <typeparam name="TU">Dönüştürülecek olan hedef nesne tipidir. (Type)</typeparam>
        /// <param name="source">Dönüştürülecek olan nesnedir.(Source)</param>
        /// <returns>Hedef nesne tipinde dönüştürülen nesnedir.</returns>
        public static TU ConvertWithoutNullValues<T, TU>(T source, TU destination)
            where T : class, new()
            where TU : class, new()
        {
            var sourceProperties = source.GetType().GetProperties().ToList();

            var destinationItem = new TU();
            var destinationProperties = destinationItem.GetType().GetProperties().ToList();

            foreach (var destinationProperty in destinationProperties)
            {
                try
                {
                    var sourceProperty = sourceProperties.Find(item => item.Name == destinationProperty.Name);

                    if (sourceProperty != null)
                    {
                        var propVal = sourceProperty.GetValue(source, null);

                        if (propVal != null)
                        {
                            destinationProperty.SetValue(destinationItem, propVal, null);
                        }
                        else
                        {
                            var propValDest = destinationProperty.GetValue(destination, null);
                            destinationProperty.SetValue(destinationItem, propValDest, null);
                        }
                    }
                    else
                    {
                        var propVal = destinationProperty.GetValue(destination, null);
                        destinationProperty.SetValue(destinationItem, propVal, null);
                    }

                }
                catch (ArgumentException)
                {
                }
            }

            return destinationItem;
        }

        /// <summary>
        /// Bu fonksiyon bir nesne listesini başka bir nesne listesine dönüştürmeye yarar. Dönüştürülecek olan nesne içerisinde bulunan 
        /// propertylere göre hedef nesneye dönüştürülür. 
        /// Kaynak property adı ve hedef property adı aynı olmak zorundadır (Büyük küçük harf duyarlılığı vardır).
        /// Propertysi eşleşmeyen veriler dönüştürülmez.
        /// </summary>
        /// <typeparam name="T">Dönüştürülmek istenen kaynak nesne tipidir. (Type)</typeparam>
        /// <typeparam name="TU">Dönüştürülecek olan hedef nesne tipidir. (Type)</typeparam>
        /// <param name="source">Dönüştürülecek olan nesne listesidir. (Source)</param>
        /// <returns>Hedef nesne tipinde dönüştürülen listedir.</returns>
        public static IEnumerable<TU> ConvertList<T, TU>(IEnumerable<T> source)
            where T : class, new()
            where TU : class, new()
        {
            return ConvertList<T, TU>(source, null);
        }

        /// <summary>
        /// Bu fonksiyon bir nesne listesini başka bir nesne listesine dönüştürmeye yarar. Dönüştürülecek olan nesne içerisinde bulunan 
        /// propertylere göre hedef nesneye dönüştürülür. 
        /// Kaynak property adı ve hedef property adı aynı olmak zorundadır (Büyük küçük harf duyarlılığı vardır).
        /// Propertysi eşleşmeyen veriler dönüştürülmez.
        /// </summary>
        /// <typeparam name="T">Dönüştürülmek istenen kaynak nesne tipidir. (Type)</typeparam>
        /// <typeparam name="TU">Dönüştürülecek olan hedef nesne tipidir. (Type)</typeparam>
        /// <param name="source">Dönüştürülecek olan nesne listesidir. (Source)</param>
        /// <param name="excludeList">Nesnenin içerisinde dönüştürülmesi istenmeyen propertyler varsa liste şeklinde verilir.</param>
        /// <returns>Hedef nesne tipinde dönüştürülen listedir.</returns>
        public static IEnumerable<TU> ConvertList<T, TU>(IEnumerable<T> source, IEnumerable<string> excludeList)
            where T : class, new()
            where TU : class, new()
        {

            var tempSource = new T();
            var sourceProperties = tempSource.GetType().GetProperties().ToList();

            var tempDestination = new TU();
            var destinationProperties = tempDestination.GetType().GetProperties().ToList();

            List<TU> destination = new List<TU>();

            foreach (var sourceItem in source)
            {
                var subDestination = ConvertList<T, TU>(sourceItem, sourceProperties, destinationProperties, excludeList);

                destination.Add(subDestination);
            }

            return destination;
        }

        private static TU ConvertList<T, TU>(T source, List<PropertyInfo> sourceProperties, List<PropertyInfo> destinationProperties, IEnumerable<string> excludeList)
            where T : class, new()
            where TU : class, new()
        {
            var destinationItem = new TU();

            foreach (var sourceProperty in sourceProperties)
            {
                if (excludeList != null)
                {
                    if (excludeList.Contains(sourceProperty.Name.ToLower()))
                    {
                        continue;
                    }
                }

                var destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    try
                    {
                        var propVal = sourceProperty.GetValue(source, null);
                        destinationProperty.SetValue(destinationItem, propVal, null);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }

            return destinationItem;
        }
    }
}
