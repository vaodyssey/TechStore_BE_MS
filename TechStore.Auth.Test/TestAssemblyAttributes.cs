using Xunit;
[assembly: TestCollectionOrderer("TechStore.Auth.Test.Orderers.CollectionOrderer", "TechStore.Auth.Test")]
[assembly: CollectionBehavior(DisableTestParallelization = true)]