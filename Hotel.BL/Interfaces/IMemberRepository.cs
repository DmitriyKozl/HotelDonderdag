using Hotel.BL.Model;

namespace Hotel.BL.Interfaces; 

public interface IMemberRepository {
    void AddMember(Member member, int customerid);
    void UpdateMember(Member member, int customerid,string name);
    void DeleteMember(Member member,int customerid);
    List<Member> GetMembers(int customerid);

}