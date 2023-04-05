using System.Collections.Generic;

namespace Features.Shed.Upgrade
{
    internal interface IUpgradeHandlersRepository : IRepository
    {
        IReadOnlyDictionary<string, IUpgradeHandler> Items { get; }
    }

    internal class UpgradeHandlersRepository
        : BaseRepository<string, IUpgradeHandler, UpgradeItemConfig>, IUpgradeHandlersRepository
    {
        private SpeedUpgradeHandler _speedUpgradeHandler;
        private JumpHeightUpgradeHandler _jumpHeightUpgradeHandler;
        private StubUpgradeHandler _stubUpgradeHandler;
        
        public UpgradeHandlersRepository(IEnumerable<UpgradeItemConfig> configs) : base(configs)
        { }

        protected override string GetKey(UpgradeItemConfig config) =>
            config.Id;

        protected override IUpgradeHandler CreateItem(UpgradeItemConfig config)
        {
            switch (config.Type)
            {
                case UpgradeType.Speed:
                    _speedUpgradeHandler = new SpeedUpgradeHandler(config.Value);
                    return _speedUpgradeHandler;
                case UpgradeType.JumpHeight:
                    _jumpHeightUpgradeHandler = new JumpHeightUpgradeHandler(config.Value);
                    return _jumpHeightUpgradeHandler;
                default:
                    _stubUpgradeHandler = new StubUpgradeHandler();
                    return _stubUpgradeHandler;
            }
        }
    }
}
