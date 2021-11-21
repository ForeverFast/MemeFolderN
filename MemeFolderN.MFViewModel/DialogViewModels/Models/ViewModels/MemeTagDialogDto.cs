using System;

namespace MemeFolderN.MFViewModels.Common.DialogViewModels
{
    public class MemeTagDialogVM : OnPropertyChangedClass
    {
        public Guid Id { get => _id; set => SetProperty(ref _id, value); }

        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public bool Selected { get => _selected; set => SetProperty(ref _selected, value); }

        public int GetHC { get => this.GetHashCode(); }

        public override string ToString() => this.Title;

        #region Поля для хранения значений свойств
        private Guid _id;
        private string _title;
        private bool _selected;
        #endregion
    }
}
