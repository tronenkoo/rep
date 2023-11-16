using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace AT2Recycle
{
    /// <summary>
    /// This method represents a recycler with properties for Name, Address, Phone, WebSite, Recycles
    /// </summary>
    public class Recycler : IComparable<Recycler>
    {
        // Properties to store information about the recycler
        public string Name { get; set; }       // The name of the recycler
        public string Address { get; set; }    // The address of the recycler
        public string Phone { get; set; }      // The phone number of the recycler
        public string WebSite { get; set; }    // The website of the recycler
        public string Recycles { get; set; }   // The types of materials the recycler accepts


        /// <summary>
        /// Parameterized constructor for initializing Recycler object with specified values
        /// </summary>
        /// <param name="name">The name of the recycler</param>
        /// <param name="adress">The address of the recycler</param>
        /// <param name="phone">The phone of the recycler</param>
        /// <param name="webSite">The webSite of the recycler</param>
        /// <param name="recycles">The recycles of the recycler</param>
        public Recycler (string name, string adress, string phone, string webSite, string recycles)
            {
                Name = name;
                Address = adress;
                Phone = phone;
                WebSite = webSite;
                Recycles = recycles;
            }

        /// <summary>
        /// This method override parametres Name, Phone, WebSite to String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Concatenate important properties for display
            string recyclerString = Name + "\t" + Phone + "\t" + WebSite;
            return recyclerString;

        }

        /// <summary>
        /// This method compares the current Recycler object to another Recycler object based on their names
        /// </summary>
        /// <param name="obj">The Recycler object to compare with</param>
        /// <returns></returns>
        public int CompareTo(Recycler obj)
        {
            // Compare Recycler objects based on their names
            return Name.CompareTo(obj.Name);
        }
    }
}
