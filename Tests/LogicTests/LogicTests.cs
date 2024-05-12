﻿using Data;
using Logic;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace LogicTests
{
    public class TestBall : IBall
    {
        public float x { get; set; }
        public float y { get; set; }
        
        private float Xvelocity;
        private float Yvelocity;
        private float density;
        private float size;

        public event PropertyChangedEventHandler PropertyChanged;

        public TestBall(float x, float y, float Xvelocity, float Yvelocity)
        {
            this.x = x;
            this.y = y;
            this.Xvelocity = Xvelocity;
            this.Yvelocity = Yvelocity;
        }

        public float getXVelocity()
        {
            return Xvelocity;
        }
        public float getYVelocity()
        {
            return Yvelocity;
        }
        public void setXVelocity(float xVelocity)
        {
            this.Xvelocity=xVelocity;
        }
        public void setYVelocity(float yVelocity)
        {
            this.Yvelocity = yVelocity;
        }

        public float getSize()
        {
            throw new NotImplementedException();
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public float getMass()
        {
            return (float)(4 / 3 * Math.PI * Math.Pow(size, 3)) * density; // TODO
        }

        public void updatePosition()
        {
            x += getXVelocity();
            y += getYVelocity();
        }
    }

    [TestFixture]
    public class LogicTests
    {
        [Test]
        public void changeXdirectionTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI();
            TestBall[] balls = new TestBall[1];
            TestBall ball = new TestBall(50,50,0.5f,0.5f);
            balls[0] = ball;
            api.setBalls(balls);
            
            // Act
            Logic.Logic.changeXdirection(ball);

            // Assert
            Assert.That(ball.getXVelocity(), Is.EqualTo(-0.5f));
        }

        [Test]
        public void changeYdirectionTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI();
            TestBall[] balls = new TestBall[1];
            TestBall ball = new TestBall(50, 50, 0.5f, 0.5f);
            balls[0] = ball;
            api.setBalls(balls);

            // Act
            Logic.Logic.changeYdirection(ball);

            // Assert
            Assert.That(ball.getYVelocity(), Is.EqualTo(-0.5f));
        }

        [Test]
        public void updatePositionTest()
        {
            // Arrange
            LogicAbstractAPI api = LogicAbstractAPI.CreateLogicAPI();
            TestBall[] balls = new TestBall[1];
            TestBall ball = new TestBall(50, 50, 0.5f, 0.5f);
            balls[0] = ball;
            api.setBalls(balls);

            // Act
            ball.updatePosition();

            // Assert
            Assert.That(ball.x, Is.EqualTo(50.5f));
            Assert.That(ball.y, Is.EqualTo(50.5f));
        }
    }
}