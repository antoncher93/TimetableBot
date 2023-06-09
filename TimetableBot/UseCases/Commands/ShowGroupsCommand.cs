﻿using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class ShowGroupsCommand
{
    public ShowGroupsCommand(
        List<Group> groups,
        Student student,
        int course)
    {
        Groups = groups;
        Student = student;
        Course = course;
    }
    
    public interface IHandler
    {
        Task HandleAsync(
            ShowGroupsCommand command);
    }
    public List<Group> Groups { get; }
    
    public Student Student { get; }
    
    public int Course { get; }
}