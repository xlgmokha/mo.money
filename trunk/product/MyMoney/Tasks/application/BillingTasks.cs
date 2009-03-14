using System.Collections.Generic;
using Castle.Core;
using MoMoney.Domain.accounting.billing;
using MoMoney.Domain.Core;
using MoMoney.Domain.repositories;
using MoMoney.Infrastructure.interceptors;
using MoMoney.Presentation.Presenters.billing.dto;

namespace MoMoney.Tasks.application
{
    public interface IBillingTasks
    {
        void save_a_new_bill_using(add_new_bill_dto dto);
        IEnumerable<IBill> all_bills();
        void register_new_company(register_new_company dto);
        IEnumerable<ICompany> all_companys();
    }

    [Interceptor(typeof (IUnitOfWorkInterceptor))]
    public class BillingTasks : IBillingTasks
    {
        readonly IBillRepository bills;
        readonly ICompanyRepository companys;
        readonly ICustomerTasks tasks;

        public BillingTasks(IBillRepository bills, ICompanyRepository companys, ICustomerTasks tasks)
        {
            this.bills = bills;
            this.companys = companys;
            this.tasks = tasks;
        }

        public void save_a_new_bill_using(add_new_bill_dto dto)
        {
            var company = companys.find_company_named(dto.company_name);
            var customer = tasks.get_the_current_customer();
            company.issue_bill_to(customer, dto.due_date, dto.total.as_money());
        }

        public IEnumerable<IBill> all_bills()
        {
            return bills.all();
        }

        public void register_new_company(register_new_company dto)
        {
            new Company(dto.company_name);
        }

        public IEnumerable<ICompany> all_companys()
        {
            return companys.all();
        }
    }
}