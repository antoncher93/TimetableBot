using Telegram.Bot;
using Telegram.Bot.Hosting;
using TimetableBot.Infrastructure;
using TimetableBot.UseCases.CommandHandlers;
using TimetableBot.UseCases.QueryHandlers;

namespace TimetableBot;

public static class ApplicationRoot
{
    public static IBotFacade CreateBotFacade(
        ITelegramBotClient client,
        string yandexDiskFolder)
    {
        var dataProvider = DataProvider.Create(
            folder: yandexDiskFolder,
            excelFileReader: new ExcelFileReader());

        var coursesRepository = new CoursesRepository(
            dataProvider: dataProvider);


        var studentRepository = new StudentRepository();

        var coursesQuery = new CoursesQueryHandler(
            coursesRepository: coursesRepository);

        var registerStudentQueryHandler = new RegisterStudentQueryHandler(
            studentRepository: studentRepository);

        var telegramBotClientAdapter = new TelegramBotClientAdapter(client);

        var showCoursesCommandHandler = new ShowCoursesCommandHandler(
            adapter: telegramBotClientAdapter);

        var groupsQueryHandler = new GroupsQueryHandler(
            groupsRepository: new GroupsRepository(
                dataProvider: dataProvider));

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
            studentRepository: studentRepository);

        var tokensRepository = new TokensRepository();

        var addAdminCommandHandler = new AddAdminCommandHandler(
            adapter: telegramBotClientAdapter,
            tokensRepository: tokensRepository);

        var joinCommandHandler = new JoinCommandHandler(
            adapter: telegramBotClientAdapter,
            tokensRepository: tokensRepository);

        var showTimetableTypesCommandHandler = new ShowTimetableTypesCommandHandler(
            adapter: telegramBotClientAdapter);
        
        return new BotFacade(
            coursesQuery: coursesQuery,
            groupsQueryHandler: groupsQueryHandler,
            registerStudentQueryHandler: registerStudentQueryHandler,
            showCoursesCommandHandler: showCoursesCommandHandler,
            showGroupsCommandHandler: showGroupsCommandHandler,
            showWeeksCommandHandler: showWeeksCommandHandler,
            showTimetableCommandHandler: showTimetableCommandHandler,
            sendMessageCommandHandler: sendMessageCommandHandler,
            addAdminCommandHandler: addAdminCommandHandler,
            joinCommandHandler: joinCommandHandler,
            showTimetableTypesCommandHandler: showTimetableTypesCommandHandler);
    }
}