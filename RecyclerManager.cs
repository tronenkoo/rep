using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AT2Recycle
{
    /// <summary>
    /// This method contains all logic operations related to adding, removing, updating, searching and file operations
    /// </summary>
    public class RecyclerManager
    {
        /// <summary>
        /// This property stores the actual list of recycler objects
        /// </summary>
        protected List<Recycler> Recyclers { get; set; } = new List<Recycler>();

        /// <summary>
        /// This method adds new recycler object to the list
        /// </summary>
        /// <param name="recycler">This is a new object to be added to the list</param>
        public void Add(Recycler recycler)
        {
            Recyclers.Add(recycler);
        }

        /// <summary>
        /// This method remove existing recycler from the list
        /// </summary>
        /// <param name="recycler">This is the existing object to me removed from the list</param>
        public void Remove(Recycler recycler)
        {
            Recyclers.Remove(recycler);
        }

        /// <summary>
        /// This method update existing recycler from the list
        /// </summary>
        /// <param name="recyclerOld">This is the existing object to be changed from the list</param>
        /// <param name="recyclerNew">This is a new object to be inserted into the list instead of the updated one</param>
        public void Update(Recycler recyclerOld, Recycler recyclerNew)
        {
            var index = Recyclers.IndexOf(recyclerOld);
            Recyclers.RemoveAt(index);
            Recyclers.Insert(index, recyclerNew);
        }

        /// <summary>
        /// Retrieves all Recycles object in the list
        /// </summary>
        /// <returns>A list contains all recycler object</returns>
        public List<Recycler> GetAll()
        {
            return Recyclers;
        }

        /// <summary>
        /// This method search object by name from the list
        /// </summary>
        /// <param name="name">This is the name of existing recycler</param>
        /// <param name="startIndex">This is the index to start search</param>
        /// <returns></returns>
        public Recycler SearchByName(string name, int startIndex)
        {
            var searchList = Recyclers;

            // Use BinarySearch instead of Contains
            int index = searchList.BinarySearch(new Recycler(name, "", "", "", ""), new RecyclerNameComparer());

            if (index >= 0)
            {
                // Exact match found
                return searchList[index];
            }
            else
            {
                // No exact match, but can use the result of BinarySearch to find the nearest match
                int nearestIndex = ~index;

                // Ensure the nearestIndex is within bounds
                if (nearestIndex > 0 && nearestIndex < searchList.Count)
                {
                    var nearestRecycler = searchList[nearestIndex];

                    // Check if the nearestRecycler.Name starts with the search term
                    if (nearestRecycler.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                    {
                        return nearestRecycler;
                    }
                }
            }

            // Return null if no match is found
            return null;
        }

        // Comparer for BinarySearch
        public class RecyclerNameComparer : IComparer<Recycler>
        {
            public int Compare(Recycler x, Recycler y)
            {
                return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
            }
        }


        /// <summary>
        /// This method gets list of object by choosen product
        /// </summary>
        /// <param name="product">This is the name of the product to filter Recyclers by</param>
        /// <returns></returns>
        public List<Recycler> GetByProduct(string product)
        {
            return Recyclers.Where(r => r.Recycles.Contains(product, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        /// <summary>
        /// This method save recyclers from list into CSV file Recyclers
        /// </summary>
        /// <param name="filename">This is a name of file where to save all recyclers from list</param>
        public void SaveAsCsv(string filename)
        {
            try
            {
                using (var writer = new StreamWriter(filename))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false
                    };

                    using (var csv = new CsvWriter(writer, csvConfig))
                    {
                        csv.WriteRecords(Recyclers);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data to CSV: {ex.Message}");
            }
        }

        /// <summary>
        /// This method reads recyclers from csv file and populate its properties (Name, Address, Phone, WebSite, Recyclers) from csv file
        /// </summary>
        /// <param name="filename">This is a name of file from which data will be read</param></param>
        public void ReadFromCsv(string filename)
        {
            try
            {
                Recyclers.Clear();

                using (var reader = new StreamReader(filename))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    while (csv.Read())
                    {
                        var recycler = new Recycler(
                            csv.GetField<string>(0),
                            csv.GetField<string>(1),
                            csv.GetField<string>(2),
                            csv.GetField<string>(3),
                            csv.GetField<string>(4)
                        );

                        Recyclers.Add(recycler);
                    }
                }
                Recyclers = Recyclers.OrderBy(r => r.Name).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data from CSV: {ex.Message}");
            }
        }
    }
}
