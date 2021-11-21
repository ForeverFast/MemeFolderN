using System;

namespace MemeFolderN.MFViewModels.Common.DialogViewModels
{
    public record MemeTagDialogDto
    {
        public Guid Id { get; init; }

        public string Title { get; init; }

        public bool Selected { get; init; }

        public int GetHC { get => this.GetHashCode(); }

        public override string ToString() => this.Title;
    }
}
