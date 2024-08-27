using Core.Utilities.Results.Abstract;
using Entities.DTOs.SearchDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISearchService
    {
        IDataResult<List<SearchResDTO>> SearchByCriteries(SearchCriteriaDTO criteria);
    }
}
