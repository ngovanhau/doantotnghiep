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

        public async Task<List<ProblemDto>> GetAllProblem()
        {
            var problems = await _ProblemRepository.GetAllProblem();
            var result = new List<ProblemDto>();

            foreach (var problem in problems) 
            {
                var image = await _ProblemRepository.GetImagesByProblem(problem.Id);

                var dto = _mapper.Map<ProblemDto>(problem);

                dto.image = image;
                result.Add(dto);
            }
            return result;
        }

        public async Task<ProblemDto> GetByIDProblem(Guid ProblemId)
        {
            var Problem = await _ProblemRepository.GetByIDProblem(ProblemId);

            if (Problem == null)
            {
                throw new NonAuthenticateException();
            }

            var image = await _ProblemRepository.GetImagesByProblem(ProblemId);

            var dto = _mapper.Map<ProblemDto>(Problem);

            if (image != null && image.Count > 0)
            {
                dto.image = image;
            }
            return dto;
        }

        public async Task<List<ProblemDto>> GetProblemByRoomId (Guid id)
        {
            var problems = await _ProblemRepository.GetByRoomId(id);
            var result = new List<ProblemDto>();

            if (problems == null)
            {
                throw new NonAuthenticateException("not found");
            }


            foreach (var problem in problems) 
            {
                var image = await _ProblemRepository.GetImagesByProblem(problem.Id);

                var dto = _mapper.Map<ProblemDto>(problem);

                dto.image = image;

                result.Add(dto);
            }   
            return result;
        }
        public async Task<Problem> CreateProblemAsync(ProblemDto dto)
        {
            var Problem = _mapper.Map<Problem>(dto);

            Problem.Id = Guid.NewGuid();

            await _ProblemRepository.CreateAsync(Problem);

            if (dto.image != null && dto.image.Count > 0) 
            {
                await _ProblemRepository.AddImagesForProblem(Problem.Id, dto.image);
            }

            return Problem;
        }
        public async Task<Problem> UpdateProblemAsync(Guid id, ProblemDto dto)
        {
            var existingProblem = await _ProblemRepository.GetByIDProblem(id);

            if (existingProblem == null)
            {
                throw new Exception("not found");
            }
            var Problem = _mapper.Map(dto, existingProblem);

            await _ProblemRepository.RemoveImagesFromProblem(id);

            await _ProblemRepository.UpdateAsync(Problem);

            if (dto.image != null && dto.image.Count > 0)
            {
                await _ProblemRepository.AddImagesForProblem(id, dto.image);
            }

            return Problem;
        }
        public async Task DeleteProblemAsync(Guid id)
        {
            var Problem = await _ProblemRepository.GetByIDProblem(id);
            if (Problem == null)
            {
                throw new Exception("Problem not found !");
            }
            await _ProblemRepository.RemoveImagesFromProblem(id);

            await _ProblemRepository.DeleteAsync(Problem);
        }

    }
}

