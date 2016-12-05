using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage;
using Windows.UI.Popups;

namespace SocialEating.ViewModel
{
    class PlaningViewModel : INotifyPropertyChanged
    {
        StorageFolder localfolder = null;
        private readonly string filnavn = "JsonText.json";

        public Model.Planing Newplan { get; set; }
        public AddPlanCommand AddPlaningCommand { get; set; }
        public RemovePlanCommand RemovePlaningCommand { get; set; }
        public SavePlanCommand SavePlaningCommand { get; set; }
        public HentPlanCommand LoadPlaningCommand { get; set; }
        public Model.PlaningList planListe { get; set; }



        private Model.Planing _selectedWod;
        public event PropertyChangedEventHandler PropertyChanged;

        public PlaningViewModel()
        {
            Newplan = new Model.Planing();
            AddPlaningCommand = new AddPlanCommand(AddNewWod);
            planListe = new Model.PlaningList();
            _selectedWod = new Model.Planing();
            RemovePlaningCommand = new RemovePlanCommand(RemoveThisWod);
            SavePlaningCommand = new SavePlanCommand(GemDataTilDiskAsync);
            LoadPlaningCommand = new HentPlanCommand(HentDataFraDiskAsync);
            localfolder = ApplicationData.Current.LocalFolder;
            HentDataFraDiskAsync();
        }

        public Model.Planing SelectedWod
        {
            get { return _selectedWod; }
            set
            {
                _selectedWod = value;
                OnPropertyChanged(nameof(SelectedWod));
            }
        }

        public async void HentDataFraDiskAsync()
        {
            // this.Wodliste.Clear();
            try
            {
                StorageFile file = await localfolder.GetFileAsync(filnavn);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.planListe.Clear();
                planListe.IndsetJson(jsonText);
            }
            catch (Exception)
            {
                /*  MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "Filnavn");
                  await messageDialog.ShowAsync(); */
            }
        }

        /// <summary>
        /// Gemmer json data fra liste i localfolder
        /// </summary>
        public async void GemDataTilDiskAsync()
        {
            string jsonText = this.planListe.GetJson();
            StorageFile file = await localfolder.CreateFileAsync(filnavn, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonText);
        }

        public void AddNewWod()
        {
            var tempWod = new Model.Planing();
            tempWod.menu = Newplan.menu;
            planListe.Add(tempWod);
        }

        public void RemoveThisWod()
        {
            planListe.Remove(SelectedWod);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
