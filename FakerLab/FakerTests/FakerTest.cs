using Xunit;
using System;
using System.Collections.Generic;

using FakerLib;

using FakerTests.Mocks;
using FieldCreators.PrimitiveTypesCreators;
using FieldCreators;

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


        [Fact]
        public void TestBoolFromDll()
        {
            ClassWithBool instance = faker.create<ClassWithBool>();
            Assert.True(instance.boolean == false || instance.boolean == true);
        }

        [Fact]
        public void TestConfigOfFaker()
        {
            var config = new FakerConfig();
            config.add<TestClassWithString, string, DefaultStringCreator>(instance => instance.str);
        
            faker = new Faker(config);

            TestClassWithString instance = faker.create<TestClassWithString>();
            Assert.Equal("default", instance.str);

        }

    }
}
