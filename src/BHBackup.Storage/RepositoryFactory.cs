using BHBackup.Storage.Repositories;

namespace BHBackup.Storage;

public sealed class RepositoryFactory
{

    public RepositoryFactory(string rootFolder, bool roundtrip)
    {
        this.RootFolder = rootFolder ?? throw new ArgumentNullException(rootFolder);
        this.Roundtrip = roundtrip;
    }

    public string RootFolder {
        get;
    }

    public bool Roundtrip
    {
        get;
    }

    public ChildNoteRepository GetChildNoteRepository()
    {
        return new ChildNoteRepository(this.RootFolder, this.Roundtrip);
    }

    public ChildSummaryRepository GetChildSummaryRepository()
    {
        return new ChildSummaryRepository(this.RootFolder, this.Roundtrip);
    }

    public FeedItemRepository GetFeedItemRepository()
    {
        return new FeedItemRepository(this.RootFolder, this.Roundtrip);
    }

    public IdentityRepository GetIdentityRepository()
    {
        return new IdentityRepository(this.RootFolder, this.Roundtrip);
    }

    public LearningJourneyRepository GetLearningJourneyRepository()
    {
        return new LearningJourneyRepository(this.RootFolder, this.Roundtrip);
    }

    public ObservationRepository GetObservationRepository()
    {
        return new ObservationRepository(this.RootFolder, this.Roundtrip);
    }

    public SidebarRepository GetSidebarRepository()
    {
        return new SidebarRepository(this.RootFolder, this.Roundtrip);
    }

}
