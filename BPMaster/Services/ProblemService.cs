using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;

namespace BPMaster.Services
{
    [ScopedService]
    public class ProblemService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly ProblemRepository _ProblemRepository = new(connection);

        public async Task<List<Problem>> GetAllProblem()
        {
            return await _ProblemRepository.GetAllProblem();
        }

        public async Task<Problem> GetByIDProblem(Guid ProblemId)
        {
            var Problem = await _ProblemRepository.GetByIDProblem(ProblemId);

            if (Problem == null)
            {
                throw new NonAuthenticateException();
            }
            return Problem;
        }

        public async Task<Problem> CreateProblemAsync(ProblemDto dto)
        {
            var Problem = _mapper.Map<Problem>(dto);

            Problem.Id = Guid.NewGuid();

            await _ProblemRepository.CreateAsync(Problem);

            return Problem;
        }
        public async Task<Problem> UpdateProblemAsync(Guid id, ProblemDto dto)
        {
            var existingProblem = await _ProblemRepository.GetByIDProblem(id);

            if (existingProblem == null)
            {
                throw new Exception("Error");
            }
            var Problem = _mapper.Map(dto, existingProblem);

            await _ProblemRepository.UpdateAsync(Problem);

            return Problem;
        }
        public async Task DeleteProblemAsync(Guid id)
        {
            var Problem = await _ProblemRepository.GetByIDProblem(id);
            if (Problem == null)
            {
                throw new Exception("Problem not found !");
            }
            await _ProblemRepository.DeleteAsync(Problem);
        }

    }
}

