﻿using TimetableBot.Models;

namespace TimetableBot.UseCases.Adapters;

public interface ITelegramBotClientAdapter
{
    Task ShowCoursesAsync(
        Student student,
        List<string> courses);

    Task ShowGroupsAsync(
        Student student,
        List<Group> groups,
        int course);

    Task ShowWeeksAsync(
        Student student,
        int course,
        int group);

    Task ShowTimetableAsync(
        long chatId,
        List<StudyDay> days,
        int course,
        int group);
    
    Task SendCopyOfMessageToAllAsync(
        long fromChatId,
        int messageId,
        long chatId);

    Task SendTokenAsync(
        long chatId,
        string token);
    
    Task SendAdminJoinedAsync(
        Student commandStudent);
    
    Task SendCannotJoinAsAdminAsync(
        Student commandStudent);

    Task ShowTimetableTypesAsync(
        long chatId,
        int course,
        int group);

    Task ShowNoTimetableForDayAsync(
        long chatId,
        DateTime date,
        int course,
        int group);
    
    Task ShowNoTimetableAsync(
        long chatId,
        int course,
        int group);

    Task ShowDayTimetableAsync(
        long chatId,
        DateTime date,
        StudyDay studyDay,
        int course,
        int group);

    Task SendAdminHasBeenDeletedAsync(long chatId);
    
    Task NotifyMessageWasSentAsync(long chatId);
    
    Task SendGroupDoesNotExistsAsync(long chatId, string groupName);
}