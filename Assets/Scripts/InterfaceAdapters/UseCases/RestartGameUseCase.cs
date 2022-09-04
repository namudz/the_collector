using Game;
using Game.Signals;
using Services.EventDispatcher;

namespace InterfaceAdapters.UseCases
{
    public class RestartGameUseCase : IRestartGameUseCase
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IGame _game;

        public RestartGameUseCase(IEventDispatcher eventDispatcher, IGame game)
        {
            _eventDispatcher = eventDispatcher;
            _game = game;
        }
        
        public void RestartGame()
        {
            _eventDispatcher.Dispatch(new GameResetSignal());
            _game.Reset();
            _eventDispatcher.Dispatch(new LoadMazeItemsToRestartSignal());
            
            _game.GetReady();
        }
    }
}