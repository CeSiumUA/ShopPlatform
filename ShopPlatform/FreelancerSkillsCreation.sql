CREATE TABLE FreelancerSkillRates
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CandidateId] [int] REFERENCES FreelancerUsers (Id) NOT NULL,
	[Skill] [nvarchar] (100) NULL,
	[Rate] [int] NULL
)