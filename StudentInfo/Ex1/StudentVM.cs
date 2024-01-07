namespace Ex1
{
    class StudentVM
    {
        public List<Student> Students { get; set; }
        public StudentVM()
        {
            Students = new List<Student>
            {
                new Student() { Id = 1, FullName = "Nguyen Luong Khang", Major = "SE" },
                new Student() { Id = 2, FullName = "Nguyen Van A", Major = "MC" },
                new Student() { Id = 3, FullName = "Nguyen Van B", Major = "IOT" },
                new Student() { Id = 4, FullName = "Nguyen Van C", Major = "SE" },
                new Student() { Id = 5, FullName = "Nguyen Van D", Major = "MKT" },
                new Student() { Id = 6, FullName = "Nguyen Van E", Major = "MC" },
                new Student() { Id = 7, FullName = "Nguyen Van F", Major = "MC" },
                new Student() { Id = 8, FullName = "Nguyen Van G", Major = "IOT" },
                new Student() { Id = 9, FullName = "Nguyen Van H", Major = "SE" },
                new Student() { Id = 10, FullName = "Nguyen Van J", Major = "MKT" },
                new Student() { Id = 11, FullName = "Nguyen Van K", Major = "SE" },
                new Student() { Id = 12, FullName = "Nguyen Van L", Major = "MC" },
                new Student() { Id = 13, FullName = "Nguyen Van M", Major = "IOT" },
                new Student() { Id = 14, FullName = "Nguyen Van N", Major = "SE" },
                new Student() { Id = 15, FullName = "Nguyen Van O", Major = "MKT" },
                new Student() { Id = 16, FullName = "Nguyen Van P", Major = "MC" },
                new Student() { Id = 17, FullName = "Nguyen Van Q", Major = "MC" },
                new Student() { Id = 18, FullName = "Nguyen Van W", Major = "IOT" },
                new Student() { Id = 19, FullName = "Nguyen Van S", Major = "SE" },
                new Student() { Id = 20, FullName = "Nguyen Van X", Major = "MKT" }
            };
        }
    }
}
