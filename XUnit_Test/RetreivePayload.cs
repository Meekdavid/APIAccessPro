using APIAccessProDependencies.Helpers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace XUnit_Test
{
    public class RetreivePayload
    {
        #region Application Form Region
        private static string _programId { get; set; }
        public static string programId
        {
            get
            {
                return "123";
            }
            set
            {
                _programId = value;
            }
        }


        private static AddQuestionModel[] _questionModel { get; set; }
        public static AddQuestionModel[] questionModel
        {
            get
            {
                return new AddQuestionModel[]
                {
                    new AddQuestionModel
                    {
                        type = "Text",
                        question = "What is your favorite color?",
                        choice = ""
                    },
                    new AddQuestionModel
                    {
                        type = "MultipleChoice",
                        question = "Which programming languages do you know?",
                        choice = "C#,Java"
                    }
                };
            }
            set
            {
                _questionModel = value;
            }
        }


        private static PersonalInfo _personalInfo { get; set; }
        public static PersonalInfo personalInfo
        {
            get
            {
                return new PersonalInfo
                {
                    first_Name = "David",
                    last_Name = "Mboko",
                    email = "davidmboko2020@gmail.com",
                    phone = "2347059337553",
                    nationality = "Nigerian",
                    current_Residence = "Nigeria",
                    iD_Number = "A227458963210",
                    date_of_Birth = "29/05/1995",
                    gender = "Male",
                    add_a_Question = questionModel
                };
            }
            set
            {
                _personalInfo = value;
            }
        }


        private static Profile _profile { get; set; }
        public static Profile profile
        {
            get
            {
                return new Profile
                {
                    education = "Bachelor of Engineering from Divine State University",
                    experience = "7 years experience at Hollywood Techies",
                    resume = "https://example.com/resume.pdf"
                };
            }
            set
            {
                _profile = value;
            }
        }


        private static AdditionalQuestions _additionalQuestions { get; set; }
        public static AdditionalQuestions additionalQuestions
        {
            get
            {
                return new AdditionalQuestions
                {
                    about_Self = "An impact oriented software engineer ready to share common goals with company employed in",
                    select_year_of_graduation = "2019",
                    rection_from_US_Embassy = "Approved"
                };
            }
            set
            {
                _additionalQuestions = value;
            }
        }


        private static ApplicationFormDTO _applicationFormDTO { get; set; }
        public static ApplicationFormDTO applicationFormDTO
        {
            get
            {
                return new ApplicationFormDTO
                {
                    id = "1",
                    userID = "1",
                    programId = programId,
                    cover_Image = "https://example.com/profilePic.pdf",
                    personal_Information = personalInfo,
                    profile = profile,
                    additional_Questions = additionalQuestions

                };
            }
            set
            {
                _applicationFormDTO = value;
            }
        }
        #endregion

        #region Program Region

        private static AdditionalProgramInfo[] _additionalProgramInfo { get; set; }
        public static AdditionalProgramInfo[] additionalProgramInfo
        {
            get
            {
                return new AdditionalProgramInfo[]
                {
                    new AdditionalProgramInfo
                    {
                        programType = new string[] { "International Training" },
                    programStart = DateTime.Now.AddMonths(5),
                    applicationOpen = DateTime.Now.AddMonths(1),
                    applicationClose = DateTime.Now.AddMonths(2),
                    duration = "9 Months",
                    programLocation = "Virtual",
                    minimumQualification = "Bachelors",
                    maximumNumberOfApplication = "126"
                    },
                    new AdditionalProgramInfo
                    {
                        programType = new string[] { "Forex Training" },
                    programStart = DateTime.Now.AddMonths(6),
                    applicationOpen = DateTime.Now.AddMonths(2),
                    applicationClose = DateTime.Now.AddMonths(3),
                    duration = "15 Months",
                    programLocation = "Virtual",
                    minimumQualification = "Bachelors",
                    maximumNumberOfApplication = "200"
                    }
                };
            }
            set
            {
                _additionalProgramInfo = value;
            }
        }


        private static ProgramDTO _programDTO { get; set; }
        public static ProgramDTO ProgramDTO
        {
            get
            {
                return new ProgramDTO
                {
                    id = "1",
                    programId = programId,
                    programTitle = "Skills International",
                    programSummary = "Acquire international skills that are highly paying",
                    programDescription = "Description",
                    keySkillsRequired = new string[] { "Coding", "Programming" },
                    programBenefits = "",
                    applicationCriteria = "",
                    additionalProgramInfo = additionalProgramInfo
                };
            }
            set
            {
                _programDTO = value;
            }
        }

        #endregion

        #region Workflow Region

        private static Stages[] _stages { get; set; }
        public static Stages[] stages
        {
            get
            {
                return new Stages[]
                {
                    new Stages
                    {
                        stageName = "Zoom Meetup",
                        stageId = "1",
                        stageType = "Video Interview",
                        stageProps = ""
                    },
                    new Stages
                    {
                        stageName = "Shortlisted",
                        stageId = "2",
                        stageType = "Shortlisting",
                        stageProps = ""
                    }
                };
            }
            set
            {
                _stages = value;
            }
        }

        private static Stages _stage { get; set; }
        public static Stages stage
        {
            get
            {
                return new Stages
                {
                    stageName = "Zoom Meetup",
                    stageId = "1",
                    stageType = "Video Interview",
                    stageProps = ""
                };
            }
            set
            {
                _stage = value;
            }
        }


        private static WorkflowDTOHolder _workflowDTOHolder { get; set; }
        public static WorkflowDTOHolder workflowDTOHolder
        {
            get
            {
                return new WorkflowDTOHolder
                {
                    id = "1",
                    userID = "123",
                    programId = "123",
                    command = "stage",
                    stages = stages
                };
            }
            set
            {
                _workflowDTOHolder = value;
            }
        }

        #endregion

        #region Preview Region

        private static PreviewDTO _previewDTO { get; set; }
        public static PreviewDTO previewDTO
        {
            get
            {
                return new PreviewDTO
                {
                    id = "1",
                    programId = programId,
                    programTitle = "Skills International",
                    programSummary = "Acquire international skills that are highly paying",
                    programDescription = "Description",
                    keySkillsRequired = new string[] { "Coding", "Programming" },
                    programBenefits = "",
                    applicationCriteria = "",
                    additionalProgramInfo = additionalProgramInfo
                };
            }
            set
            {
                _previewDTO = value;
            }
        }

        #endregion
    }
}
