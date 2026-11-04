namespace SupportTicketingSystem.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        // ----------------------------
        public int ClientId { get; set; }

        //--------------------------
        public int ProductId { get; set; }

        // --------------------------
        public int? AssignedToId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "New";

        public DateTime CreatedAt { get; set; }

        public DateTime? AssignedAt { get; set; }

        public DateTime? AcceptedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        // ------------------------
        public User Client { get; set; } = null!;

        public Product Product { get; set; } = null!;

        public User? AssignedTo { get; set; }
    }
}