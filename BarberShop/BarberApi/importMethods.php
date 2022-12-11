<?php

function importEmploye($connect){
    if(move_uploaded_file($_FILES['Employers']['tmp_name'], '../files/'.$_FILES['Employers']['name'])){
     $responce=[
         "status"=>false,
         "message"=>"file not found"
     ];
    }

      if (!file_exists('../files/'.$_FILES['Employers']['name'])) {
          $responce=[
              "status"=>false,
              "message"=>"file not found"
          ];
          echo json_encode($responce); die();
      }
     try{
         $file =fopen("../files/".$_FILES['Employers']['name'], 'r');     
         $counter =0;       
         while (!feof($file)){
             $employer = fgetcsv($file,1024 ,';');
                //print_r( $employer);
             //print_r($employer);die();
             //$i = count($employer);
             if ($employer[1]!=null){
                 $createEmployee=$connect->prepare(
                     "insert into Employe( FirstName, LastName, MiddleName, Email, Password, INN, Post_ID, Status_ID) VALUES (?,?,?,?,?,?,?,?)");
                    // print_r($employer);
                     //if(empty($data["middlename"])) $data["middlename"]="-";
                     $createEmployee->execute(array(
                         strval($employer[1]), //firstName
                         strval($employer[2]), //lastname
                         strval($employer[3]), //middlename
                         strval($employer[4]),  //email
                         strval($employer[5]), //password
                         strval($employer[6]), //inn
                         $employer[7], //post_id
                         $employer[8] //status_id
                     ));


                     //print_r($employer); die();
                     $counter++;

             }
         }
         $responce=[
             "status"=>true,
             "message"=>"$counter user imported"
         ];
         echo json_encode($responce);
     } catch(PDOException  $e){
         $responce=[
             "status"=>false,
             "message"=>"employers are not imported"
         ];
         echo json_encode($responce);
     }
     fclose($file);
   //  unlink('../files/'.$_FILES['Employers']['name']);
 }
//////////////



function executeBackup($connect){
    // нужно будет доделать
   // echo "awertyuik";
    try{
        //print_r($_FILES); die();
            if(!move_uploaded_file($_FILES['Employers']['tmp_name'], '../files/'.$_FILES['Employers']['name'])){
                $responce=[
                "status"=>false,
                    "message"=>"file not found"
                ];
                echo $responce; die();
            }
            if(!move_uploaded_file($_FILES['Records']['tmp_name'], '../files/'.$_FILES['Records']['name'])){
            $responce=[
                "status"=>false,
                "message"=>"file not found"
                ];
                echo $responce; die();
            }
            if(!move_uploaded_file($_FILES['Stocks']['tmp_name'], '../files/'.$_FILES['Stocks']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
                }
            if(!move_uploaded_file($_FILES['Posts']['tmp_name'], '../files/'.$_FILES['Posts']['name'])){
                    $responce=[
                        "status"=>false,
                        "message"=>"file not found"
                        ];
                        echo $responce; die();
            }
            if(!move_uploaded_file($_FILES['Clients']['tmp_name'], '../files/'.$_FILES['Clients']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }
            if(!move_uploaded_file($_FILES['Providers']['tmp_name'], '../files/'.$_FILES['Providers']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }
            
            if(!move_uploaded_file($_FILES['TaxReports']['tmp_name'], '../files/'.$_FILES['TaxReports']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }

            if(!move_uploaded_file($_FILES['Vacations']['tmp_name'], '../files/'.$_FILES['Vacations']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }
             if(!move_uploaded_file($_FILES['SickLeaves']['tmp_name'], '../files/'.$_FILES['SickLeaves']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }
            if(!move_uploaded_file($_FILES['Supplies']['tmp_name'], '../files/'.$_FILES['Supplies']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }

            if(!move_uploaded_file($_FILES['Services']['tmp_name'], '../files/'.$_FILES['Services']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }

            if(!move_uploaded_file($_FILES['Materials']['tmp_name'], '../files/'.$_FILES['Materials']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }

            if(!move_uploaded_file($_FILES['Incomes']['tmp_name'], '../files/'.$_FILES['Incomes']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }

            if(!move_uploaded_file($_FILES['Consumptions']['tmp_name'], '../files/'.$_FILES['Consumptions']['name'])){
                $responce=[
                    "status"=>false,
                    "message"=>"file not found"
                    ];
                    echo $responce; die();
            }
          //  echo "fjreiohgerg";
        //print_r($_FILES); die();
            // $deleteEmploye=$connect->prepare(
            //     "DELETE FROM `Employe` WHERE 1");
            //     $deleteEmploye->execute();

            $deleteStatus=$connect->prepare(
                "DELETE FROM `Status_Employee` WHERE 1");
            $deleteStatus->execute();

            $deleteStatus=$connect->prepare(
                "DELETE FROM `Consumption` WHERE 1");
            $deleteStatus->execute();

            $deleteRecord=$connect->prepare(
                "DELETE FROM `Record` WHERE 1");
            $deleteRecord->execute();

            $deleteStock=$connect->prepare(
                "DELETE FROM `Stock` WHERE 1");
            $deleteStock->execute();

            $deletePost=$connect->prepare(
                "DELETE FROM `Post` WHERE 1");
            $deletePost->execute();

            $deleteProvider=$connect->prepare(
                "DELETE FROM `Provider` WHERE 1");
            $deleteProvider->execute();

            $deleteTaxReports=$connect->prepare(
                "DELETE FROM `TaxReport` WHERE 1");
            $deleteTaxReports->execute();

            $deleteService=$connect->prepare(
                "DELETE FROM `Service` WHERE 1");
            $deleteTaxReports->execute();

            $deleteIncome=$connect->prepare(
                "DELETE FROM `Income` WHERE 1");
            $deleteIncome->execute();

            $deleteIncome=$connect->prepare(
                "DELETE FROM `Client` WHERE 1");
            $deleteIncome->execute();
            $createStatus=$connect->prepare(
                    "insert into Status_Employee(ID_Status, Name_Status) values (1,'Работает'),(2,'Уволен'),(3,'В отпуске'),(4,'На больничном');");
            $createStatus->execute();

            $file = fopen("../files/".$_FILES['Posts']['name'], 'r');
                   
            while (!feof($file)){
               $post = fgetcsv($file,2048 ,';');
               //print_r($post);
               if ($post[0]!=null){
                  $createPost=$connect->prepare(
                         "INSERT INTO `Post`(`ID_Post`, `Name_Post`, `Price`) VALUES (?,?,?)");
                  $createPost->execute(array(
                             $post[0], //id
                             strval($post[1]),//addrenamw_post
                             str_replace(",",".", $post[2])//post_price
                         ));
                 }
             }
            fclose($file);

            $file =fopen("../files/".$_FILES['Employers']['name'], 'r');   
                   // $arrayEmployers=array();
                   // $i<=count($employer = fgetcsv($file,2048 ,';'));
            while (!feof($file)){
                $employer = fgetcsv($file,2048 ,';');
                        //  $employees_array = null;
                        //  $employees_array = array_push($employer)
                        //  for ($i = 0; $i <= count($employees_array); $i++) {
                        //     sendEmployee($employees_array[$i]);
                        //  }
                    
                        //    //print_r( $employer[0]);die();
                        // //print_r($_FILES);
                        // //$i = count($employer);
                        // function sendEmployee(people) {

                        // }
                if ($employer[1]!=null){
                            //$id=$employer[0];
                            //print_r( $id);die();
                $createEmployee=$connect->prepare(
                                "insert into Employe(ID_Employee, FirstName, LastName, MiddleName, Email, Password, INN, Post_ID, Status_ID) VALUES (?,?,?,?,?,?,?,?,?)");
                                //print_r($employer);
                                //if(empty($data["middlename"])) $data["middlename"]="-";
                    $createEmployee->execute(array(
                                    $employer[0], //id
                                    strval($employer[1]), //firstName
                                    strval($employer[2]), //lastname
                                    strval($employer[3]), //middlename
                                    strval($employer[4]),  //email
                                    strval($employer[5]), //password
                                    strval($employer[6]), //inn
                                    $employer[7], //post_id
                                    $employer[8] //status_id
                                ));
           
           
                                //print_r($employer); die();
                               
           
                        }
            }
            fclose($file);
                    
            $file = fopen("../files/".$_FILES['Clients']['name'], 'r');
            while (!feof($file)){
                $client = fgetcsv($file,2048 ,';');
                      //print_r($stock[1]);
                 if ($client[0]!=null){
                    $createClient=$connect->prepare(
                                "INSERT INTO `Client`(`ID_Client`, `First_Name`, `Last_Name`, `Middle_Name`, `Phone`, `Email`, `Password`)
                                 VALUES (?,?,?,?,?,?,?)");
                    $createClient->execute(array(
                                    $client[0], //id
                                    strval($client[1]),//firstname
                                    strval($client[2]),//lastname
                                    strval($client[3]),//middlename
                                    strval($client[4]),//phone
                                    strval($client[5]),//email
                                    strval($client[6])//password
                                ));
                        }
            }
            fclose($file);


            $file = fopen("../files/".$_FILES['Records']['name'], 'r');
            while (!feof($file)){
                $record = fgetcsv($file,2048 ,';');
                if ($record[1]!=null){
                    $createRecord=$connect->prepare(
                               "INSERT INTO `Record`(`ID_Record`,`Date_Record`, `Service_ID`, `Client_ID`)  VALUES (?,?,?,?)");
                               
                    $createRecord->execute(array(
                                   $record[0], //id
                                   date_format(date_create($record[1]), 'Y-m-d H:i:s'), //date_record
                                   strval($record[2]), //service_id
                                   strval($record[3])// client_id
                               ));
          
          
                               //print_r($employer); die();
                              
          
                       }
            }
            fclose($file);
             
            
            $file = fopen("../files/".$_FILES['Stocks']['name'], 'r');
            while (!feof($file)){
                $stock = fgetcsv($file,2048 ,';');
                      //print_r($stock[1]);
                 if ($stock[0]!=null){
                    $createStock=$connect->prepare(
                                "INSERT INTO `Stock`(`ID_Stock`, `Adres`) VALUES (?,?)");
                    $createStock->execute(array(
                                    $stock[0], //id
                                    strval($stock[1])//address
                                ));
                        }
            }
            fclose($file);

            $file = fopen("../files/".$_FILES['Providers']['name'], 'r');
            while (!feof($file)){
                $provider = fgetcsv($file,2048 ,';');
                      //print_r($stock[1]);
                 if ($provider[0]!=null){
                    $createProvider=$connect->prepare(
                                "INSERT INTO `Provider`(`ID_Provider`, `Adres`, `Name_Provider`, `INN`)
                                 VALUES (?,?,?,?)");
                    $createProvider->execute(array(
                                    $provider[0], //id
                                    strval($provider[1]),//address
                                    strval($provider[2]),
                                    strval($provider[3])
                                ));
                        }
            }
            fclose($file);

            $file = fopen("../files/".$_FILES['TaxReports']['name'], 'r');
        
            while (!feof($file)){
                $report = fgetcsv($file,2048 ,';');
                      //print_r($stock[1]);
                 if ($report[0]!=null){
                    $createReport=$connect->prepare(
                                "INSERT INTO `TaxReport`(`ID_TaxReport`, `DateReport`, `Date_Begin`, `Date_End`, `ValueSells`, `ValueTax`, `Employe_ID`)
                                 VALUES (?,?,?,?,?,?,?)");
                    $createReport->execute(array(
                                    $report[0], //id
                                    date_format(date_create($report[1]), 'Y-m-d H:i:s'),
                                    date_format(date_create($report[2]), 'Y-m-d H:i:s'),
                                    date_format(date_create($report[3]), 'Y-m-d H:i:s'),
                                    $report[4],//valueSells
                                    $report[5],//ValueTax
                                    $report[6] //employe_id
                                ));
                        }
            }
            fclose($file);
                   

            $file = fopen("../files/".$_FILES['Vacations']['name'], 'r');
        
            while (!feof($file)){
                $vacation = fgetcsv($file,2048 ,';');
                      //print_r($stock[1]);
                 if ($vacation[0]!=null){
                    $createVacation=$connect->prepare(
                                "INSERT INTO `Vacation`(`ID_Vacation`, `Date_Begin`, `Date_End`, `Employe_ID`)
                                 VALUES (?,?,?,?)");
                    $createVacation->execute(array(
                                    $vacation[0], //id
                                    date_format(date_create($vacation[1]), 'Y-m-d H:i:s'),
                                    date_format(date_create($vacation[2]), 'Y-m-d H:i:s'),
                                    $vacation[3] //employe_id
                                ));
                        }
            }
            fclose($file);

            $file = fopen("../files/".$_FILES['SickLeaves']['name'], 'r');
        
            while (!feof($file)){
                $sickLeave = fgetcsv($file,2048 ,';');
                      //print_r($stock[1]);
                 if ($sickLeave[0]!=null){
                    $createSickLeave=$connect->prepare(
                                "INSERT INTO `SickLeave`(`ID_SickLeave`, `Date_Begin`, `Date_End`, `Employe_ID`)
                                 VALUES (?,?,?,?)");
                    $createSickLeave->execute(array(
                                    $sickLeave[0], //id
                                    date_format(date_create($sickLeave[1]), 'Y-m-d H:i:s'),
                                    date_format(date_create($sickLeave[2]), 'Y-m-d H:i:s'),
                                    $sickLeave[3] //employe_id
                                ));
                        }
            }
            fclose($file);

            $file = fopen("../files/".$_FILES['Supplies']['name'], 'r');
        
            while (!feof($file)){
                $supply = fgetcsv($file,2048 ,';');
                      //print_r($stock[1]);
                 if ($supply[0]!=null){
                    $createSupply=$connect->prepare(
                                "INSERT INTO `Supply`(`ID_Supply`, `Date_Supply`, `Value`, `Provider_ID`)
                                 VALUES (?,?,?,?)");
                    $createSupply->execute(array(
                                    $supply[0], //id
                                    date_format(date_create($supply[1]), 'Y-m-d H:i:s'),
                                    $supply[2],
                                    $supply[3] //provider_id
                                ));
                        }
            }
            fclose($file);


            $file = fopen("../files/".$_FILES['Services']['name'], 'r');
        
            while (!feof($file)){
                $service = fgetcsv($file,2048 ,';');
                  //    print_r($service);
                 if ($service[0]!=null){
                    $createService=$connect->prepare(
                                "INSERT INTO `Service`(`ID_Service`, `Name_Service`, `Price_Service`)
                                 VALUES (?,?,?)");
                    $createService->execute(array(
                                    $service[0], //id
                                    $service[1],
                                    $service[2] 
                                ));
                        }
            }
            fclose($file);

            $file = fopen("../files/".$_FILES['Materials']['name'], 'r');
        
            while (!feof($file)){
                $material = fgetcsv($file,2048 ,';');
                  //    print_r($service);
                 if ($material[0]!=null){
                    $createMaterial=$connect->prepare(
                                "INSERT INTO `Material`(`ID_Material`, `Name_Material`, `Stock_ID`, `Supply_ID`)
                                 VALUES (?,?,?,?)");
                    $createMaterial->execute(array(
                                    $material[0], //id
                                    $material[1],
                                    $material[2],
                                    $material[3]  
                                ));
                        }
            }
            fclose($file);

            $file = fopen("../files/".$_FILES['Incomes']['name'], 'r');
        
            while (!feof($file)){
                $income = fgetcsv($file,2048 ,';');
                  //    print_r($service);
                 if ($income[0]!=null){
                    $createIncome=$connect->prepare(
                                "INSERT INTO `Income`(`ID_Income`, `Date_Income`, `Value`) 
                                 VALUES (?,?,?)");
                    $createIncome->execute(array(
                                    $income[0], //id
                                    date_format(date_create($income[1]), 'Y-m-d H:i:s'),
                                    $income[2]
                                ));
                        }
            }
            fclose($file);
            $file = fopen("../files/".$_FILES['Consumptions']['name'], 'r');
        
            while (!feof($file)){
                $consumption = fgetcsv($file,2048 ,';');
                  //    print_r($service);
                 if ($consumption[0]!=null){
                    $createConsumption=$connect->prepare( //Data_Consumption
                                "INSERT INTO `Consumption`(`ID_Consumption`, `Data_Consumption`, `Value`) 
                                 VALUES (?,?,?)");
                    $createConsumption->execute(array(
                                    $consumption[0], //id
                                    date_format(date_create($income[1]), 'Y-m-d H:i:s'),
                                    $consumption[2]
                                ));
                        }
            }
            fclose($file);

            unlink("../files/".$_FILES['Consumptions']['name']);
            unlink("../files/".$_FILES['Materials']['name']);
            unlink("../files/".$_FILES['Services']['name']);
            unlink("../files/".$_FILES['Supplies']['name']);
            unlink("../files/".$_FILES['TaxReports']['name']);
            unlink("../files/".$_FILES['Clients']['name']);
            unlink("../files/".$_FILES['Stocks']['name']);
            unlink("../files/".$_FILES['Posts']['name']);
            unlink("../files/".$_FILES['Employers']['name']);
            unlink("../files/".$_FILES['Vacations']['name']);
            unlink("../files/".$_FILES['SickLeaves']['name']);
            unlink("../files/".$_FILES['Records']['name']);
            unlink("../files/".$_FILES['Providers']['name']);
            unlink("../files/".$_FILES['Incomes']['name']);
            
    } catch (PDOException  $e){

    }
}