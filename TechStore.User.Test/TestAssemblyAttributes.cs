using Xunit;
[assembly: TestCollectionOrderer("TechStore.User.Test.Orderers.CollectionOrderer", "TechStore.User.Test")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]