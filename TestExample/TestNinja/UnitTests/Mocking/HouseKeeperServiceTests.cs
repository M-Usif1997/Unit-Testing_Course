using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace UnitTests.Mocking
{

    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private string _statemenFileName;
        private Mock<IUnitOfWork> _unitOfWork;
        private Housekeeper _houseKeeper;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private HousekeeperService _service;
        private DateTime _statementDate = new DateTime(2025, 1, 1);

        [SetUp]

        public void SetUp()
        {

            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
               _houseKeeper,
            }
             .AsQueryable());

            _statemenFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg =>
                 sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                //for Lazy evaluation
                .Returns(()=> _statemenFileName);
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();


            _service = new HousekeeperService(
             _unitOfWork.Object,
             _statementGenerator.Object,
             _emailSender.Object,
             _xtraMessageBox.Object);



        }
        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
            sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));

        }



        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]

        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatements(string email)
        {
            _houseKeeper.Email = email;
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg =>
            sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);

        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es =>
            es.EmailFile(_houseKeeper.Email, _houseKeeper.StatementEmailBody, _statemenFileName,
              It.IsAny<string>()));


        }



        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]

        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailTheStatement(string fileName)
        {
            _statemenFileName = fileName;

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es =>
            es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);

        }

        [Test]
        
        public void SendStatementEmails_EmailSendFails_DisplayAMessageBox()
        {
           _emailSender.Setup(es =>
            es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            _xtraMessageBox.Verify(xm => xm.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));

        }
    }
}
