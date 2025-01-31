USE [opurple]
GO
/****** Object:  User [GG_ServiceOperation]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_ServiceOperation] FOR LOGIN [LOCAWEB-NET\GG_ServiceOperation]
GO
/****** Object:  User [GG_Suporte_AEP]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_Suporte_AEP] FOR LOGIN [LOCAWEB-NET\GG_Suporte_AEP]
GO
/****** Object:  User [GG_Suporte_Cloud_Dedicado]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_Suporte_Cloud_Dedicado] FOR LOGIN [LOCAWEB-NET\GG_Suporte_Cloud_Dedicado]
GO
/****** Object:  User [GG_Suporte_Hospedagem]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_Suporte_Hospedagem] FOR LOGIN [LOCAWEB-NET\GG_Suporte_Hospedagem]
GO
/****** Object:  User [GG_Suporte_Plesk11]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_Suporte_Plesk11] FOR LOGIN [LOCAWEB-NET\GG_Suporte_Plesk11]
GO
/****** Object:  User [GG_Suporte_Plesk115]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_Suporte_Plesk115] FOR LOGIN [LOCAWEB-NET\GG_Suporte_Plesk115]
GO
/****** Object:  User [GG_Suporte_Plesk12]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_Suporte_Plesk12] FOR LOGIN [LOCAWEB-NET\GG_Suporte_Plesk12]
GO
/****** Object:  User [GG_SysOps_CorporateServices]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_SysOps_CorporateServices] FOR LOGIN [LOCAWEB-NET\GG_SysOps_CorporateServices]
GO
/****** Object:  User [GG_SysOps_Windows]    Script Date: 15/03/2019 18:29:54 ******/
CREATE USER [GG_SysOps_Windows] FOR LOGIN [LOCAWEB-NET\GG_SysOps_Windows]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_ServiceOperation'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_Suporte_AEP'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_Suporte_Cloud_Dedicado'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_Suporte_Hospedagem'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_Suporte_Plesk11'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_Suporte_Plesk115'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_Suporte_Plesk12'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_SysOps_CorporateServices'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'GG_SysOps_Windows'
GO
/****** Object:  Table [dbo].[AMG]    Script Date: 15/03/2019 18:29:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AMG](
	[AMG_ID] [int] IDENTITY(1,1) NOT NULL,
	[AMG_USR_ID] [int] NULL,
	[AMG_USR_USR_ID] [int] NULL,
	[AMG_SLC] [int] NULL,
	[AMG_OK] [int] NULL,
	[AMG_DAT_INC] [datetime] NULL,
	[AMG_DAT_ALT] [datetime] NULL,
	[AMG_DAT_EXL] [datetime] NULL,
 CONSTRAINT [PK_AMG] PRIMARY KEY CLUSTERED 
(
	[AMG_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FED]    Script Date: 15/03/2019 18:29:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FED](
	[FED_ID] [int] IDENTITY(1,1) NOT NULL,
	[FED_USR_ID] [int] NULL,
	[FED_POT] [nvarchar](200) NULL,
	[FED_ALB_ID] [int] NULL,
	[FED_FOT_ID] [int] NULL,
	[FED_DAT_INC] [datetime] NULL,
	[FED_DAT_ALT] [datetime] NULL,
 CONSTRAINT [PK_FED] PRIMARY KEY CLUSTERED 
(
	[FED_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FOT]    Script Date: 15/03/2019 18:29:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FOT](
	[FOT_ID] [int] IDENTITY(1,1) NOT NULL,
	[FOT_USR_ID] [int] NULL,
	[FOT_PAT] [nvarchar](200) NULL,
	[FOT_TIT] [nvarchar](200) NULL,
	[FOT_LEG] [nvarchar](200) NULL,
	[FOT_DAT_INC] [datetime] NULL,
 CONSTRAINT [PK_FOT] PRIMARY KEY CLUSTERED 
(
	[FOT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USR]    Script Date: 15/03/2019 18:29:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USR](
	[USR_ID] [int] IDENTITY(1,1) NOT NULL,
	[USR_NOM] [nvarchar](100) NULL,
	[USR_DAT_NASC] [datetime] NULL,
	[USR_SEX] [nvarchar](10) NULL,
	[USR_EMA] [nvarchar](200) NULL,
	[USR_FOT] [nvarchar](200) NULL,
	[USR_SNH] [nvarchar](200) NULL,
	[USR_DAT_INC] [datetime] NULL,
	[USR_DAT_EXL] [datetime] NULL,
	[USR_DAT_ALT] [datetime] NULL,
	[USR_EML_VLD] [int] NULL,
 CONSTRAINT [PK_USR] PRIMARY KEY CLUSTERED 
(
	[USR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
