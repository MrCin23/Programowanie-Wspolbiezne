using Data;
using Logic;
using NUnit.Framework;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace LogicTests
{
    public class TestBall : IBall
    {
        public Vector2 pos { get; set; }

        public Vector2 vel { get ; set; }

        private float density;
        private float size;

        public event EventHandler<DataEventArgs>? ChangedPosition;

        public TestBall(float x, float y, float Xvelocity, float Yvelocity)
        {
            Vector2 pos = new Vector2(x, y);
            Vector2 vel = new Vector2(Xvelocity, Yvelocity);
            this.pos = pos; this.vel = vel;
        }

        public float getSize()
        {
            throw new NotImplementedException();
        }


        public float getMass()
        {
            return (float)(4 / 3 * Math.PI * Math.Pow(size, 3)) * density; // TODO
        }
        public void move()
        {
            this.pos += vel;
            DataEventArgs args = new DataEventArgs(pos);
            ChangedPosition?.Invoke(this, args);
        }

        public void destroy()
        {
            throw new NotImplementedException();
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
            Assert.That(ball.vel.X, Is.EqualTo(-0.5f));
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
            Assert.That(ball.vel.Y, Is.EqualTo(-0.5f));
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
            ball.move();

            // Assert
            Assert.That(ball.pos.X, Is.EqualTo(50.5f));
            Assert.That(ball.pos.Y, Is.EqualTo(50.5f));
        }
    }
}