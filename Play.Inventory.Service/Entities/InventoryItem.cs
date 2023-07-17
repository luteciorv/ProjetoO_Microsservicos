using Play.Commom.Entities;

namespace Play.Inventory.Service.Entities
{
    public class InventoryItem : IEntity
    {
        public InventoryItem(Guid userId, Guid itemId, int amount)
        {
            UserId = userId;
            ItemId = itemId;
            Amount = amount;
            AcquiredDate = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set;}

        public Guid UserId { get; private set; }
        public Guid ItemId { get; private set; }
        public int Amount { get; private set; }
        public DateTimeOffset AcquiredDate { get; private set; }

        /// <summary>
        /// Incrementa/adiciona a quantidade especificada do item no inventário do jogador
        /// </summary>
        public void Stack(int amount) =>
            Amount += amount;
    }
}
