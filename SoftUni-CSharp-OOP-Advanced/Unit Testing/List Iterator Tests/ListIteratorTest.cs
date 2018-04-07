namespace ListIteratorTests
{
    using ListIterator;
    using NUnit.Framework;

    public class ListIteratorTest
    {
        private string[] testData = new string[3];
        private Iterator iterator;

        [SetUp]
        public void InitializeIterator()
        {
            this.testData = new[] {"Alex", "Sandra", "Dominik"};
            this.iterator = new Iterator(this.testData);
        }

        [Test]
        public void ThrowExceptionOnInitWhenArrayIsNull()
        {
            Assert.That(() => this.iterator = new Iterator(null), Throws.ArgumentNullException);
        }

        [Test]
        public void MoveShouldReturnTrueIfMovingToNextPositionIsPossible()
        {
            Assert.That(() => this.iterator.Move(), Is.EqualTo(true));
            Assert.That(() => this.iterator.Move(), Is.EqualTo(true));
        }

        [Test]
        public void MoveShouldReturnFalseIfMovingToNextPositionIsNotPossible()
        {
            this.iterator.Move();
            this.iterator.Move();
            this.iterator.Move();

            Assert.That(() => this.iterator.HasNext(), Is.EqualTo(false));
        }

        [Test]
        public void HasNextShouldReturnTrueIfThereIsANextPosition()
        {
            Assert.That(() => this.iterator.Move(), Is.EqualTo(true));
            Assert.That(() => this.iterator.Move(), Is.EqualTo(true));
        }

        [Test]
        public void HasNextShouldReturnFalseIfThereIsNoNextPosition()
        {
            this.iterator.Move();
            this.iterator.Move();
            this.iterator.Move();

            Assert.That(() => this.iterator.HasNext(), Is.EqualTo(false));
        }

        [Test]
        public void PrintThrowsInvalidOperationExceptionWhenArrayIsEmpty()
        {
            this.iterator = new Iterator();

            Assert.That(() => this.iterator.Print(), Throws.InvalidOperationException);
        }

        [Test]
        public void PrintShouldPrintStringAtDesiredPosition()
        {
            this.iterator.Move();
            
            Assert.That(() => this.iterator.Print(), Is.EqualTo("Sandra"));
        }
    }
}