﻿using System.Collections.Generic;
using Shouldly;
using TestStack.Dossier.Tests.TestHelpers.Builders;
using TestStack.Dossier.Tests.TestHelpers.Objects.Entities;
using Xunit;

namespace TestStack.Dossier.Tests
{
    public class BuildTests
    {
        [Fact]
        public void GivenBasicBuilder_WhenCallingBuildExplicitly_ThenReturnAnObject()
        {
            var builder = new BasicCustomerBuilder();

            var customer = builder.Build();

            customer.ShouldBeOfType<Customer>();
        }

        [Fact]
        public void GivenBuilderWithMethodCalls_WhenCallingBuildExplicitly_ThenReturnAnObjectWithTheConfiguredParameters()
        {
            var builder = new CustomerBuilder()
                .WithFirstName("Matt")
                .WithLastName("Kocaj")
                .WhoJoinedIn(2010);

            var customer = builder.Build();

            customer.FirstName.ShouldBe("Matt");
            customer.LastName.ShouldBe("Kocaj");
            customer.YearJoined.ShouldBe(2010);
        }

        [Fact]
        public void GivenBuilder_WhenCallingSetExplicitly_ShouldOverrideValues()
        {
            var builder = new CustomerBuilder()
                .Set(x => x.FirstName, "Pi")
                .Set(x => x.LastName, "Lanningham")
                .Set(x => x.YearJoined, 2014);

            var customer = builder.Build();

            customer.FirstName.ShouldBe("Pi");
            customer.LastName.ShouldBe("Lanningham");
            customer.YearJoined.ShouldBe(2014);
        }

        [Fact]
        public void GivenBuilder_WhenCallingSetWithLambda_ShouldInvokeEachTime()
        {
            int counter = 2014;
            var builder = new CustomerBuilder()
                .Set(x => x.FirstName, "Pi")
                .Set(x => x.LastName, "Lanningham")
                .Set(x => x.YearJoined, () => counter++);

            var customerA = builder.Build();
            var customerB = builder.Build();

            customerA.YearJoined.ShouldBe(2014);
            customerB.YearJoined.ShouldBe(2015);

            List<Customer> customerList = CustomerBuilder.CreateListOfSize(10)
                                            .All()
                                                .Set(x => x.YearJoined, () => counter++);
            int newCounter = 2016;
            foreach (var c in customerList)
            {
                c.YearJoined.ShouldBe(newCounter++);
            }

        }

        [Fact]
        public void GivenBasicBuilder_WhenCallingBuildImplicitly_ThenReturnAnObject()
        {
            Customer customer = new BasicCustomerBuilder(); 

            customer.ShouldBeOfType<Customer>();
        }

        [Fact]
        public void GivenBuilderWithMethodCalls_WhenCallingBuildImplicitly_ThenReturnAnObjectWithTheConfiguredParameters()
        {
            Customer customer = new CustomerBuilder()
                .WithFirstName("Matt")
                .WithLastName("Kocaj")
                .WhoJoinedIn(2010);

            customer.FirstName.ShouldBe("Matt");
            customer.LastName.ShouldBe("Kocaj");
            customer.YearJoined.ShouldBe(2010);
        }

        [Fact]
        public void GivenBuilder_WhenCallingSetImplicitly_ShouldOverrideValues()
        {
            Customer customer = new CustomerBuilder()
                .Set(x => x.FirstName, "Pi")
                .Set(x => x.LastName, "Lanningham")
                .Set(x => x.YearJoined, 2014);

            customer.FirstName.ShouldBe("Pi");
            customer.LastName.ShouldBe("Lanningham");
            customer.YearJoined.ShouldBe(2014);
        }

        [Fact]
        public void GivenBuilderUsingConstructorReflection_WhenCallingBuildExplicitly_ShouldOverrideValues()
        {
            Customer customer = new AutoConstructorCustomerBuilder()
                .WithFirstName("Bruce")
                .WithLastName("Wayne")
                .WhoJoinedIn(2012)
                .Build();

            customer.FirstName.ShouldBe("Bruce");
            customer.LastName.ShouldBe("Wayne");
            customer.YearJoined.ShouldBe(2012);
        }
    }
}