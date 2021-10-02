using Xunit;
using System;
using System.Collections.Generic;

using FakerLib;

using FakerTests.Mocks;

namespace FakerTests
{
    public class FakerTest
    {
        private Faker faker;

        public FakerTest()
        {
            this.faker = new Faker();
        }

     
        [Fact]
        public void TestCycle()
        {
            A a = faker.create<A>();
            Assert.NotNull(a.bClass);

            Assert.NotNull(a.bClass.cClass);
            Assert.Null(a.bClass.aClass);

        }

        [Fact]
        public void TestListCreator()
        {
            C c = faker.create<C>();
            Assert.NotNull(c.list);
        }

        [Fact]
        public void TestClassWithPrivateConstuctorWithoutFullParameters()
        {
            ClassWithConstructor c = faker.create<ClassWithConstructor>();
            Assert.NotNull(c.c);
        }

    }
}
