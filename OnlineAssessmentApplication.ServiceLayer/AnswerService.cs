using AutoMapper;
using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.Repository;
using OnlineAssessmentApplication.ViewModel;
using System.Collections.Generic;

namespace OnlineAssessmentApplication.ServiceLayer
{
    public interface IAnswerService
    {
        void InsertAnswer(AnswerViewModel answerView);
        IEnumerable<AnswerViewModel> DisplayAnswers(int questionId);
        void EditAnswer(AnswerViewModel editedData);
        void DeleteAnswer(int answerId);
        AnswerViewModel GetAnswersByQuestionID(int answerId);
    }
    public class AnswerService : IAnswerService
    {
        AnswerRepository answerRepository;
        public AnswerService()
        {
            answerRepository = new AnswerRepository();
        }
        public void InsertAnswer(AnswerViewModel answerView)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<AnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer answer = mapper.Map<AnswerViewModel, Answer>(answerView);
            answerRepository.InsertAnswer(answer);
        }
        public IEnumerable<AnswerViewModel> DisplayAnswers(int questionId)
        {
            IEnumerable<Answer> answers = answerRepository.DisplayAnswers(questionId);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            IEnumerable<AnswerViewModel> allAnswers = mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerViewModel>>(answers);
            return allAnswers;
        }
        public void EditAnswer(AnswerViewModel editedData)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<AnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer editAnswer = mapper.Map<AnswerViewModel, Answer>(editedData);
            answerRepository.EditAnswer(editAnswer);
        }
        public void DeleteAnswer(int answerId)
        {
            answerRepository.DeleteAnswer(answerId);
        }

        public AnswerViewModel GetAnswersByQuestionID(int answerId)
        {
            Answer answer = answerRepository.GetAnswersByQuestionID(answerId);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            AnswerViewModel OriginalData = mapper.Map<Answer, AnswerViewModel>(answer);
            return OriginalData;
        }
    }
}
