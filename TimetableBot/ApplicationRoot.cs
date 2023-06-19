using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Hosting;
using TimetableBot.Infrastructure;
using TimetableBot.UseCases.CommandHandlers;
using TimetableBot.UseCases.Queries;
using TimetableBot.UseCases.QueryHandlers;

namespace TimetableBot;

public static class ApplicationRoot
{
    public static IBotFacade CreateBotFacade(
        ITelegramBotClient client,
        string sqlConnectionString)
    {
        var dataProvider = DataProvider.Create(
            excelFileReader: new ExcelFileReader());

        var coursesRepository = new CoursesRepository(
            dataProvider: dataProvider);

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(connectionString: sqlConnectionString);

        var db = new ApplicationDbContext(options: builder.Options);

        var studentRepository = new StudentRepository(db);
        
        var adminRepository = new AdminRepository(db);

        var coursesQuery = new CoursesQueryHandler(
            coursesRepository: coursesRepository);

        var registerStudentQueryHandler = new RegisterStudentQueryHandler(
            studentRepository: studentRepository,
            adminRepository: adminRepository);

        var telegramBotClientAdapter = new TelegramBotClientAdapter(client);

        var showCoursesCommandHandler = new ShowCoursesCommandHandler(
            adapter: telegramBotClientAdapter);

        var groupsRepository = new GroupsRepository(
            dataProvider: dataProvider);

        var groupsQueryHandler = new GroupsQueryHandler(
            groupsRepository: groupsRepository);

        var showGroupsCommandHandler = new ShowGroupsCommandHandler(
            adapter: telegramBotClientAdapter);

        var showWeeksCommandHandler = new ShowWeeksCommandHandler(
            adapter: telegramBotClientAdapter);

        var studyDaysRepository = new StudyDaysRepository(dataProvider);

        var showTimetableCommandHandler = new ShowTimetableCommandHandler(
            adapter: telegramBotClientAdapter,
            studyDaysRepository: studyDaysRepository);

        var sendMessageCommandHandler = new SendMessageCommandHandler(
            adapter: telegramBotClientAdapter,
            studentRepository: studentRepository,
            coursesRepository: coursesRepository,
            groupsRepository: groupsRepository);

        var addAdminCommandHandler = new AddAdminCommandHandler(
            adapter: telegramBotClientAdapter,
            adminRepository: adminRepository);

        var joinCommandHandler = new JoinAdminCommandHandler(
            adapter: telegramBotClientAdapter,
            adminRepository: adminRepository);

        var showTimetableTypesCommandHandler = new ShowTimetableTypesCommandHandler(
            adapter: telegramBotClientAdapter);

        var isUserAdminQueryHandler = new IsUserAdminQueryHandler(
            adminRepository: adminRepository);

        var deleteAdminCommandHandler = new DeleteAdminCommandHandler(
            clientAdapter: telegramBotClientAdapter,
            adminRepository: adminRepository);

        var setStudentGroupCommandHandler = new SetStudentGroupCommandHandler(
            studentRepository: studentRepository,
            groupsRepository: groupsRepository);
        
        return new BotFacade(
            coursesQuery: coursesQuery,
            isUserAdminQueryHandler: isUserAdminQueryHandler,
            groupsQueryHandler: groupsQueryHandler,
            registerStudentQueryHandler: registerStudentQueryHandler,
            showCoursesCommandHandler: showCoursesCommandHandler,
            showGroupsCommandHandler: showGroupsCommandHandler,
            showWeeksCommandHandler: showWeeksCommandHandler,
            showTimetableCommandHandler: showTimetableCommandHandler,
            sendMessageCommandHandler: sendMessageCommandHandler,
            addAdminCommandHandler: addAdminCommandHandler,
            joinCommandHandler: joinCommandHandler,
            showTimetableTypesCommandHandler: showTimetableTypesCommandHandler,
            deleteAdminCommandHandler: deleteAdminCommandHandler,
            setStudentGroupCommandHandler: setStudentGroupCommandHandler);
    }
}