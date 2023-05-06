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
        string sqlConnectionString,
        string yandexDiskFolder)
    {
        var dataProvider = YandexCloudDataProvider.Create(
            folder: yandexDiskFolder,
            excelFileReader: new ExcelFileReader());

        var coursesRepository = new CoursesRepository(
            dataProvider: dataProvider);

        var dbConnectionFactory = new SqlConnectionFactory(sqlConnectionString);

        var studentRepository = new StudentRepository(
            dbConnectionFactory: dbConnectionFactory);

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
        
        return new BotFacade(
            coursesQuery: coursesQuery,
            groupsQueryHandler: groupsQueryHandler,
            registerStudentQueryHandler: registerStudentQueryHandler,
            showCoursesCommandHandler: showCoursesCommandHandler,
            showGroupsCommandHandler: showGroupsCommandHandler);
    }
}