﻿using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class CircularCloudLayouterTests
{
    [Test]
    public void Constructor_Success_OnPointArgument()
    {
        var a = () => new CircularCloudLayouter(Point.Empty);

        a.Should().NotThrow();
    }
    
    [Test]
    public void PutNextRectangle_ReturnsRectangle_WithProvidedSize()
    {
        var center = Point.Empty;
        var layouter = new CircularCloudLayouter(center);
        var rectSize = new Size(2, 2);

        layouter.PutNextRectangle(rectSize).Size.Should().Be(rectSize);
    }
    
    [Test]
    public void PutNextRectangle_ReturnsRectangleInCenter_OnFirstInvoke()
    {
        var center = Point.Empty;
        var layouter = new CircularCloudLayouter(center);
        var rectSize = new Size(2, 2);
        var centerWithOffset = new Point(center.X - rectSize.Width / 2, center.Y - rectSize.Height / 2);

        layouter.PutNextRectangle(rectSize).Location.Should().Be(centerWithOffset);
    }

    [Test]
    public void PutNextRectangle_ReturnsUniqueRectangles()
    {
        var center = Point.Empty;
        var layouter = new CircularCloudLayouter(center);
        var rectSize = new Size(2, 2);
        var iterations = 100;

        for (var i = 0; i < iterations; i++)
            layouter.PutNextRectangle(rectSize);
        
        layouter.Rectangles.Should().OnlyHaveUniqueItems();
    }
    
    [Test]
    public void PutNextRectangle_ReturnsRectangles_WithoutIntersections()
    {
        var center = Point.Empty;
        var layouter = new CircularCloudLayouter(center);
        var rectSize = new Size(2, 2);
        var iterations = 100;

        for (var i = 0; i < iterations; i++)
            layouter.PutNextRectangle(rectSize);

        layouter.Rectangles.All(rect => layouter.Rectangles.Except(new []{rect}).All(y => !y.IntersectsWith(rect))).Should().BeTrue();
    }

    [Test]
    public void PutNextRectangle_CreatesTagCloud_WithCircleShape()
    {
        
    }
}