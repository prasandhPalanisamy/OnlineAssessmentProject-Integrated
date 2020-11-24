using AutoMapper;
using OnlineAssessmentApplication.DomainModel;
using OnlineAssessmentApplication.Repository;
using OnlineAssessmentApplication.ViewModel;
using System.Collections.Generic;

namespace OnlineAssessmentApplication.ServiceLayer
{
    public interface ITestService
    {
        TestViewModel GetTestByTestId(int testId);
        IEnumerable<TestViewModel> DisplayAvailableTestDetails(FilterPanel filterPanel);
        IEnumerable<ResultViewModel> CalculateScore(ResultViewModel resultViewModel);
        bool VerifyPasscode(int passcode);
        int CreateNewTest(CreateTestViewModel testViewModel);
        void UpdateTest(EditTestViewModel editedData);
        void DeleteTest(int testId);
        void UpdateAcceptStatus(int testId);
        void UpdateRejectStatus(int testId);
    }
    public class TestService : ITestService
    {
        readonly ITestRepository testRepository;
        public TestService(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }
        public int CreateNewTest(CreateTestViewModel testViewModel)
        {

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CreateTestViewModel, Test>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var test = mapper.Map<CreateTestViewModel, Test>(testViewModel);
            return testRepository.CreateNewTest(test);

        }
        public IEnumerable<TestViewModel> DisplayAvailableTestDetails(FilterPanel filterPanel)
        {
            IEnumerable<Test> test = testRepository.DisplayAvailableTestDetails(filterPanel);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Test, TestViewModel>(); });
            IMapper mapper = config.CreateMapper();
            IEnumerable<TestViewModel> testViewModels = mapper.Map<IEnumerable<Test>, IEnumerable<TestViewModel>>(test);
            return testViewModels;
        }
        public bool VerifyPasscode(int passcode)
        {
            return testRepository.VerifyPasscode(passcode);
        }
        public void UpdateTest(EditTestViewModel editedData)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditTestViewModel, Test>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Test editTest = mapper.Map<EditTestViewModel, Test>(editedData);
            testRepository.UpdateTest(editTest);
        }
        public void DeleteTest(int testId)
        {
            testRepository.DeleteTest(testId);
        }
        public TestViewModel GetTestByTestId(int testId)
        {
            Test test = testRepository.GetTestByTestId(testId);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Test, TestViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            TestViewModel OriginalData = mapper.Map<Test, TestViewModel>(test);
            return OriginalData;
        }

        public IEnumerable<ResultViewModel> CalculateScore(ResultViewModel resultViewModel)
        {
            return testRepository.CalculateScore(resultViewModel);
        }

        public void UpdateAcceptStatus(int testId)
        { 
            testRepository.UpdateAcceptStatus(testId);
        }
        public void UpdateRejectStatus(int testId)
        {
            testRepository.UpdateRejectStatus(testId);
        }

    }
}
