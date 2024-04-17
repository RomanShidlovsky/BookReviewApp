using Book.Domain.Entities;
using Book.Domain.Interfaces.Repositories;

namespace Book.Infrastructure.Seed;

public class SubjectSeedInitializer(ISubjectRepository _subjectRepository) : ISeedInitializer
{
    public static Subject[] Subjects { get; } =
    [
        new Subject { Name = "Fiction" },
        new Subject { Name = "Action & Adventure" },
        new Subject { Name = "Science Fiction" }
    ];
    
    public void Init()
    {
        foreach (var subject in Subjects)
        {
            _subjectRepository.Create(subject);
        }
    }
}