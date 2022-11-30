```csharp
 public TIPercent ActualCPFBonusEE =>
     Family.Assigne.Age > 70 ? 
         CPF70BonusEE :
     Family.Assigne.Age > 65 ? 
         CPF65BonusEE :
     Family.Assigne.Age > 60 ? 
         CPF60BonusEE :
     Family.Assigne.Age > 55 ? 
         CPF55BonusEE : CPFBonusEE;

```

```csharp
 public TIPercent ActualCPFBonusEE =>
     Family.Assigne.Age > 70 ? CPF70BonusEE :
     Family.Assigne.Age > 65 ? CPF65BonusEE :
     Family.Assigne.Age > 60 ? CPF60BonusEE :
     Family.Assigne.Age > 55 ? CPF55BonusEE :
            CPFBonusEE;
            
```

```csharp
 public TIPercent ActualCPFBonusEE(int age) =>
      age > 70 ? CPF70BonusEE :
      age > 65 ? CPF65BonusEE :
      age > 60 ? CPF60BonusEE :
      age > 55 ? CPF55BonusEE :
          CPFBonusEE;
          
 public TIPercent ActualCPFBonusER(int age) =>
     age > 70 ? CPF70BonusER :
     age > 65 ? CPF65BonusER :
     age > 60 ? CPF60BonusER :
     age > 55 ? CPF55BonusER :
          CPFBonusER;
          
 etc.
            
 ```
 
 ```csharp
 private ITaxation PickTaxationByAge(int age, ITaxation over70, ITaxation over65, ITaxation over60, ITaxation over55, ITaxation defaultTaxation) =>
    age > 70 ? over70 :
    age > 65 ? over65 :
    age > 60 ? over60 :
    age > 55 ? over55 :
        defaultTaxation;
        
  public TIPercent
      CPF70SalaryEE, CPF65SalaryEE, CPF60SalaryEE, CPF55SalaryEE, CPFSalaryEE;
            
  public ITaxation ActualCPFSalaryEE(int age) =>
      PickTaxationByAge(age, CPF70SalaryEE, CPF65SalaryEE, CPF60SalaryEE, CPF55SalaryEE, CPFSalaryEE);
 ```
 
 ```csharp
 
 
 
 
            
   
   
