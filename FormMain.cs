using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace AT2Recycle
{
    /// <summary>
    /// Class FormMain which contains all operations on the design form
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// Initializes an instance of the RecyclerManager class to use its methods in the FormMain class
        /// </summary>
        RecyclerManager recyclerManager = new RecyclerManager();

        /// <summary>
        /// List of recycle products 
        /// </summary>
        private string[] recycleProducts= new String[] { "All Waste", "Electronic Waste", "Green Waste", "Hazardour Waste", "Household Waste", "Scrap Cars", "Scrap Metal", "Unwanted Items" };

        /// <summary>
        /// This method gets or sets the currently selected Recycler in the listBoxRecyclers
        /// </summary>
        public Recycler SelectedRecycler
        {
            get
            {
                return listBoxRecyclers.SelectedItem as Recycler;
            }
            set
            {
                listBoxRecyclers.SelectedItem = value;
            }
        }

        /// <summary>
        /// This method gets or sets the data from the CSV file of the default Recycler in the text fields
        /// </summary>
        public Recycler CurrentRecycler
        {
            get
            {
               return new Recycler(businessName_textBox.Text, adress_textBox.Text, webSite_textBox.Text, recycles_textBox.Text, phone_textBox.Text)
                {
                    Name = businessName_textBox.Text,
                    Address = adress_textBox.Text,
                    Phone = phone_textBox.Text,
                    WebSite = webSite_textBox.Text,
                    Recycles = recycles_textBox.Text,
                };
            }
            set
            {
                if (value == null)
                {
                    businessName_textBox.Text = "";
                    adress_textBox.Text = "";
                    phone_textBox.Text = "";
                    webSite_textBox.Text = "";
                    recycles_textBox.Text = "";

                    return;
                }

                businessName_textBox.Text = value.Name;
                adress_textBox.Text = value.Address;
                phone_textBox.Text = value.Phone;
                webSite_textBox.Text = value.WebSite;
                recycles_textBox.Text = value.Recycles;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the form's data has been modified
        /// </summary>
        bool IsDirty { get; set; } = false;

        /// <summary>
        /// Gets or sets the index of the current record in the data
        /// </summary>
        public int CurrentRecordIndex = 0;

        /// <summary>
        /// This method initialize the form components, clear and populate comboBox with recycleProducts
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            filterBy_comboBox.Items.Clear();
            filterBy_comboBox.Items.AddRange(recycleProducts);
        }

        /// <summary>
        /// This method read recyclers data from CSV file, update list in the form, call the base class OnLoad method
        /// </summary>
        /// <param name="e">This is the name of OnLoad method</param>
        protected override void OnLoad(EventArgs e)
        {
            recyclerManager.ReadFromCsv("recyclers.csv");

            UpdateList();

            base.OnLoad(e);
        }

        /// <summary>
        /// This method updates list after the object in comboBox was selected
        /// </summary>
        private void UpdateList()
        {
            listBoxRecyclers.DataSource = null;

            if (filterBy_comboBox.Text == "All Waste")
            {
                listBoxRecyclers.DataSource = recyclerManager.GetAll();
            }
            else
            {
                List<Recycler> filteredRecyclers = recyclerManager.GetByProduct(filterBy_comboBox.Text);
                
                listBoxRecyclers.DataSource = filteredRecyclers;
            }

            SetRecyclerByIndex();
        }

        /// <summary>
        /// This method adds a new record to the list and updates the displayed list
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void AddNewRecord_button_Click(object sender, EventArgs e)
        {
            recyclerManager.Add(CurrentRecycler);

            UpdateList();

            IsDirty = true;
        }

        /// <summary>
        /// This method close window after the exit_Button was clicked
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void exit_Button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddNewRecord_button_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentRecycler?.Name) &&
                string.IsNullOrWhiteSpace(CurrentRecycler?.Address) &&
                string.IsNullOrWhiteSpace(CurrentRecycler?.Phone) &&
                string.IsNullOrWhiteSpace(CurrentRecycler?.WebSite) &&
                string.IsNullOrWhiteSpace(CurrentRecycler?.Recycles))
                    {
                        MessageBox.Show("Please fill in the details before adding a new record.");
                        return;
                    }

            if (CurrentRecycler != null)
            {
                DialogResult result = MessageBox.Show("Do you want to continue adding the new business record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    recyclerManager.Add(CurrentRecycler);

                    UpdateList();

                    CurrentRecycler = null;
                    IsDirty = true;
                }
            }
        }

        /// <summary>
        /// This method deletes the selected recycler from the list, resets the current recycler, updates the displayed list
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void deletExistingRecord_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CurrentRecycler?.Name) &&
            string.IsNullOrWhiteSpace(CurrentRecycler?.Address) &&
            string.IsNullOrWhiteSpace(CurrentRecycler?.Phone) &&
            string.IsNullOrWhiteSpace(CurrentRecycler?.WebSite) &&
            string.IsNullOrWhiteSpace(CurrentRecycler?.Recycles))
            {
                MessageBox.Show("Please fill in the details before adding a new record.");
                return;
            }
            else
            {
                var chosenRecycler = listBoxRecyclers.SelectedItem as Recycler;

                DialogResult result = MessageBox.Show("Do you wish to continue?", "Confirmation", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // User clicked Yes, proceed with deletion
                    recyclerManager.Remove(chosenRecycler);
                    CurrentRecycler = null;
                    UpdateList();
                    IsDirty = true;
                }
            }
        }

        /// <summary>
        /// This method saves all data in CSV file after buttonSave was clicked
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        /// <summary>
        /// This method saves data in scv file
        /// </summary>
        private void SaveData()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "csv"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            IsDirty = false;

            recyclerManager.SaveAsCsv(saveFileDialog.FileName);

            Process.Start(new ProcessStartInfo { FileName = saveFileDialog.FileName, UseShellExecute = true });
        }

        /// <summary>
        /// Opens a CSV file using a file dialog, reads the data from the file, and updates the displayed list 
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { DefaultExt = "csv" };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            recyclerManager.ReadFromCsv(openFileDialog.FileName);

            UpdateList();
        }

        /// <summary>
        /// Compares the current recycler with the selected recycler. If changes are detected, updates the existing record in the list and refreshes the displayed list
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void UpdateexistingRecord_button_Click(object sender, EventArgs e)
        {
            Recycler current = CurrentRecycler;
            Recycler selected = SelectedRecycler;


            if (string.IsNullOrWhiteSpace(CurrentRecycler?.Name) &&
             string.IsNullOrWhiteSpace(CurrentRecycler?.Address) &&
             string.IsNullOrWhiteSpace(CurrentRecycler?.Phone) &&
             string.IsNullOrWhiteSpace(CurrentRecycler?.WebSite) &&
             string.IsNullOrWhiteSpace(CurrentRecycler?.Recycles))
            {
                MessageBox.Show("Please fill in the details before adding a new record.");
                return;
            }
            else
            {
                recyclerManager.Update(SelectedRecycler, CurrentRecycler);

                UpdateList();
            }
        }

        /// <summary>
        /// Event handler for the FormMain closing event,  Displays a warning message if changes haven't been saved 
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        /// <summary>
        /// Sets the CurrentRecycler property to the selected recycler in the list
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void listBoxRecyclers_Click(object sender, EventArgs e)
        {
            CurrentRecycler = SelectedRecycler;
        }

        /// <summary>
        /// Clears the CurrentRecycler, resetting the text fields in the form.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void clearFields_button_Click(object sender, EventArgs e)
        {
            CurrentRecycler = null;
        }

        /// <summary>
        /// Opens the default web browser and navigates to the website associated with the selected recycler.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void goToUrl_button_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = SelectedRecycler.WebSite, UseShellExecute = true });
        }

        /// <summary>
        /// Searches for a recycler by name using binary search, starting from the currently selected index in the list
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void binarySearch_button_Click(object sender, EventArgs e)
        {
                string searchName = enterBNtoFind_textBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(searchName))
                {
                    MessageBox.Show("Please enter a search name.");
                    return;
                }

                if (listBoxRecyclers.SelectedItem == null)
                {
                    MessageBox.Show("Please select a recycler from the list.");
                    return;
                }

                Recycler foundRecycler = recyclerManager.SearchByName(searchName, listBoxRecyclers.SelectedIndex + 1);

                if (foundRecycler != null)
                {
                    CurrentRecycler = foundRecycler;
                    SelectedRecycler = foundRecycler;
                }
                else
                {
                    MessageBox.Show("Recycler not found.");
                }
        }

            private void listBoxRecyclers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
                
        }

        /// <summary>
        /// Moves to the next record in the list, updating the CurrentRecordIndex and displaying the corresponding recycler
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (CurrentRecordIndex == listBoxRecyclers.Items.Count - 1)
                return;

            CurrentRecordIndex++;

            SetRecyclerByIndex();
        }

        /// <summary>
        /// Moves to the previous record in the list, updating the CurrentRecordIndex and displaying the corresponding recycler
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void buttonPrev_Click(object sender, EventArgs e)
        {
            //if (CurrentRecordIndex <= 0)
                //return;

            CurrentRecordIndex --;

            SetRecyclerByIndex();
        }

        /// <summary>
        /// Moves to the first record in the list, updating the CurrentRecordIndex and displaying the corresponding recycler
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void firstRecord_button_Click(object sender, EventArgs e)
        {
            CurrentRecordIndex = 0;

            SetRecyclerByIndex();
        }

        /// <summary>
        /// Sets the current and selected recycler based on the current index, updates the enabled state of navigation buttons based on the current index
        /// </summary>
        private void SetRecyclerByIndex()
        {
            if (listBoxRecyclers.Items.Count == 0)
                return;

            CurrentRecycler = recyclerManager.GetAll()[CurrentRecordIndex];
            SelectedRecycler = recyclerManager.GetAll()[CurrentRecordIndex];

            //buttonLastRecord.Visible = CurrentRecordIndex != listBoxRecyclers.Items.Count - 1;
            //buttonNext.Visible = CurrentRecordIndex != listBoxRecyclers.Items.Count - 1;
            //buttonPrev.Visible = CurrentRecordIndex != 0;
            //firstRecord_button.Visible = CurrentRecordIndex != 0;
        }

        /// <summary>
        /// Moves to the last record in the list, updating the CurrentRecordIndex and displaying the corresponding recycler
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void buttonLastRecord_Click(object sender, EventArgs e)
        {
            CurrentRecordIndex = recyclerManager.GetAll().Count - 1;

            SetRecyclerByIndex();
        }

        /// <summary>
        /// Updates the displayed list based on the selected filter option
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void filterBy_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        /// <summary>
        /// Event handler for the "Exit" button click, displays a warning message if changes haven't been saved and prompts the user to save or discard changes before closing the application
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void exit_Button_Click_1(object sender, EventArgs e)
        {
                var response = MessageBox.Show("You are to exit the application - do you wish to SAVE changes to all records?", "SAVE",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (response == DialogResult.Yes)
                {
                    SaveData();
                }

                // Close the form whether the user clicked Yes or No
                this.Close();
        }

         
        private void enterBNtoFind_textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
