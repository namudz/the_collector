namespace InterfaceAdapters.Services.GameObjectPooling
{
    public class PoolableGameObjectDto
    {
        public readonly string Id;
        public readonly GameObjectPoolCollection PoolCollection;

        public PoolableGameObjectDto(string poolId, GameObjectPoolCollection poolCollection)
        {
            Id = poolId;
            PoolCollection = poolCollection;
        }
    }
}