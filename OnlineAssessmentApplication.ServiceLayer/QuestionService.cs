using AutoMapper;
using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.Repository;
using OnlineAssessmentApplication.ViewModel;
using System.Collections.Generic;

namespace OnlineAssessmentApplication.ServiceLayer
{
    interface IQuestionService
    {
        int InsertQuestion(QuestionViewModel createQuestionsViewModel);
        void EditQuestion(QuestionViewModel editData);
        void DeleteQuestion(int questionID);
        QuestionViewModel GetQuestionsByTestId(int questionID);
        IEnumerable<QuestionViewModel> DisplayAllDetails(int testId);

    }
    public class QuestionService : IQuestionService
    {
        QuestionRepository questionRepository;
        public QuestionService()
        {
            questionRepository = new QuestionRepository();
        }
        public int InsertQuestion(QuestionViewModel questionsViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<QuestionViewModel, Questions>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Questions question = mapper.Map<QuestionViewModel, Questions>(questionsViewModel);
            return questionRepository.InsertQuestion(question);
        }
        public void EditQuestion(QuestionViewModel editData)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<QuestionViewModel, Questions>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Questions editQuestion = mapper.Map<QuestionViewModel, Questions>(editData);
            questionRepository.EditQuestion(editQuestion);
        }
        public void DeleteQuestion(int questionID)
        {
            questionRepository.DeleteQuestion(questionID);
        }
        public QuestionViewModel GetQuestionsByTestId(int questionID)
        {
            Questions question = questionRepository.GetQuestionsByTestId(questionID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Questions, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            QuestionViewModel OriginalData = mapper.Map<Questions, QuestionViewModel>(question);
            return OriginalData;
        }
        public IEnumerable<QuestionViewModel> DisplayAllDetails(int testId)
        {
            IEnumerable<Questions> questions = questionRepository.DisplayAllQuestions(testId);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Questions, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            IEnumerable<QuestionViewModel> allQuestions = mapper.Map<IEnumerable<Questions>, IEnumerable<QuestionViewModel>>(questions);
            return allQuestions;
        }
    }
}
