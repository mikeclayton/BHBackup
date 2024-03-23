using BHBackup.Client.GraphQl.LearningJourney.Models;

namespace BHBackup.Storage.Repositories;

public sealed class LearningJourneyRepository : OfflineRepository<LearningJourneyQueryResponse>
{

    internal LearningJourneyRepository(string rootFolder, bool roundtrip)
        : base(rootFolder, roundtrip)
    {
    }

    private static string GetLearningJourneyFileRootPath()
    {
        return Path.Join(
            "data", "learningjourney"
        );
    }

    private static string GettLearningJourneyFileRelativePath(string childNoteId)
    {
        return Path.Join(
            LearningJourneyRepository.GetLearningJourneyFileRootPath(),
            $"childJourney-{childNoteId}.json"
        );
    }

    #region OfflineRepository Interface

    public void Clear()
    {
        var absolutePath = base.GetAbsoluteFilename(
            LearningJourneyRepository.GetLearningJourneyFileRootPath()
        );
        if (Directory.Exists(absolutePath))
        {
            var cacheFiles = base.GetRepositoryFiles(
                LearningJourneyRepository.GetLearningJourneyFileRootPath(),
                "*.json"
            );
            foreach (var cacheFile in cacheFiles)
            {
                File.Delete(cacheFile);
            }
        }
    }

    public override IEnumerable<LearningJourneyQueryResponse> ReadAll()
    {
        Console.WriteLine("reading cached child notes...");
        var cacheFiles = base.GetRepositoryFiles(
            LearningJourneyRepository.GetLearningJourneyFileRootPath(),
            "journey-*.json"
        );
        var childJourney = cacheFiles.Select(
            cacheFile => base.ReadRepositoryJsonFile(cacheFile, true)
        );
        return childJourney;
    }

    public override LearningJourneyQueryResponse ReadItem(string id)
    {
        throw new NotImplementedException();
    }

    public override void WriteItem(LearningJourneyQueryResponse item)
    {
        throw new NotImplementedException();
    }

    public void WriteItem(LearningJourneyQueryResponse item, int index)
    {
        this.WriteRepositoryJsonFile(
            LearningJourneyRepository.GettLearningJourneyFileRelativePath($"{index:000}"),
            item
        );
    }

    #endregion

}
