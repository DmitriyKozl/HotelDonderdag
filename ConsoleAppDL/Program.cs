using Hotel.BL.Model;
using Hotel.DL.Repositories;

namespace ConsoleAppDL {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True;TrustServerCertificate=True";
            MemberRepository memberRepository = new MemberRepository(connectionString);

            // Example CustomerId
            int customerId = 44; 

            // Add a member
            // Member newMember = new Member("Dmitiry Smozlov", new DateOnly(1990, 1, 1));
            // memberRepository.AddMember(newMember, customerId);
            // Console.WriteLine("Member added.");

            // Update the member
            // Member updatedMember = new Member("Dmitiry Smozlov", new DateOnly(1990, 1, 1));
            // memberRepository.UpdateMember(updatedMember, customerId, "Dmitriy Kozlov");
            // Console.WriteLine("Member updated.");

            // Retrieve members
            List<Member> members = memberRepository.GetMembers(customerId);
            Console.WriteLine("Members:");
            foreach (var member in members)
            {
                Console.WriteLine($"{member.Name}, Birthday: {member.Birthday}");
            }

            //Delete the member
            // memberRepository.DeleteMember(updatedMember, customerId);
            // Console.WriteLine("Member deleted.");
        }
    }
}