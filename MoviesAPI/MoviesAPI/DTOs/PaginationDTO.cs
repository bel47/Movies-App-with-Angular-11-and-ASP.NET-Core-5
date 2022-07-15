namespace MoviesAPI.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int recordePerPage = 10;
        private readonly int maxAmount = 50;
        public int RecordePerPage
        {
            get
            {
                return recordePerPage;
            }
            set
            {
                recordePerPage = (value > maxAmount) ? maxAmount : value;
            }
        }

    }
}
