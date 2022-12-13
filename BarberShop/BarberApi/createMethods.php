<?php

function createPost($connect, $data){
    try{
        $selectPosts=$connect->prepare("Select * from Post where Name_Post=?");
        $selectPosts->execute(array(strval($data["name_post"])));
        if(count($selectPosts->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"post already exist"
            ];
            echo json_encode($responce);
            die();
        }



        $createPost=$connect->prepare(
        "call Post_Insert(?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $createPost->execute(array(
            strval($data["name_post"]), 
            strval($data["price"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"post created"
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"post not created"
        ];
        echo json_encode($responce);
    }
}

function createService($connect, $data){
    try{
        $select=$connect->prepare("Select * from Service where Name_Service=?");
        $select->execute(array(strval($data["name_service"])));
        if(count($select->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"Service already exist"
            ];
            echo json_encode($responce);
            die();
        }

        
        $create=$connect->prepare(
        "insert into Service (Name_Service, Price_Service) VALUES(?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $create->execute(array(
            strval($data["name_service"]), 
            strval($data["price_service"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"service created"
        ];
        echo json_encode($responce);
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"service not created"
        ];
        echo json_encode($responce);
    }
}

function createProvider($connect, $data){
    try{
        $select=$connect->prepare("Select * from Provider where Adres=? or Name_Provider=? or INN=?");
        $select->execute(array(
            strval($data["adres"]),
            strval($data["name_provider"]),
            strval($data["inn"])
    ));
        if(count($select->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"Provider already exist or data aren't unique"
            ];
            echo json_encode($responce);
            die();
        }



        $createEmploye=$connect->prepare(
        "insert into Provider (Adres, Name_Provider, INN) VALUES 
        (?, ?, ?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $createEmploye->execute(array(
            strval($data["adres"]),
            strval($data["name_provider"]),
            strval($data["inn"])
    ));
        $responce=[
            "status"=>true,
            "message"=>"Provider created",
            "id"=>$connect->lastInsertId()
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Provider not created"
        ];
        echo json_encode($responce);
    }
}

function createStock($connect, $data){
    try{
        $selectPosts=$connect->prepare("Select * from Stock where 	Adres=?");
        $selectPosts->execute(array(strval($data["adres"])));
        if(count($selectPosts->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"stock already exist"
            ];
            echo json_encode($responce);
            die();
        }



        $createEmploye=$connect->prepare(
        "insert into Stock (Adres) VALUES 
        (?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $createEmploye->execute(array(
            strval($data["adres"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"Stock created",
            "id"=>$connect->lastInsertId()
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Stock not created"
        ];
        echo json_encode($responce);
    }
}

function registrateClient($connect, $data){ /// Нужно доработать
    try{
        $selectClients=$connect->prepare("Select * from Client where Phone=? or Email=?");
        $selectClients->execute(array(
            strval($data["phone"]),
            strval($data["email"])
        ));
        if(count($selectClients->fetchAll())>0){
            $responce=[
                "status"=>false,
                "message"=>"Client already exist or data aren't unique"
            ];
            echo json_encode($responce);
            die();
        }



        $createEmploye=$connect->prepare(
        "insert into Client (First_Name, LastName, Middle_Name, Phone, Email, Password) VALUES 
        (?,?,?,?,?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $createEmploye->execute(array(
            strval($data["firstname"]),
            strval($data["lastname"]),
            strval($data["middlename"]),
            strval($data["phone"]),
            strval($data["email"]),
            md5(strval($data["password"])),
        ));
        $responce=[
            "status"=>true,
            "message"=>"Client registrated"
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Stock not created"
        ];
        echo json_encode($responce);
    }
}

function createRecord($connect, $data){
    try{
        $select=$connect->prepare("Select * from Record where Date_Record=? and Service_ID=?");
        $select->execute(array(
            strval($data["date_record"]),
            strval($data["service_id"])
        ));
        $counter=count($select->fetchAll());
       // echo($counter); die();
        if($counter>4){
            $responce=[
                "status"=>false,
                "message"=>"too many records on this date and time to selected service"
            ];
            echo json_encode($responce);
            die();
        }
        //print_r($data); die();
      //  echo(count($select->fetchAll()));die();

        $select2=$connect->prepare("Select * from Record where Date_Record=? and Client_ID=?");
        $select2->execute(array(
            strval($data["date_record"]),
            strval($data["client_id"])
        ));
        $counter =count($select2->fetchAll());
      //  echo($counter);die();
        if($counter>10){
            $responce=[
                "status"=>false,
                "message"=>"this client already have record on this time"
            ];
            echo json_encode($responce);
            die();
        }


        $createEmploye=$connect->prepare(
        "insert into Record (Date_Record, Service_ID , Client_ID) VALUES 
        (?,?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $createEmploye->execute(array(
            strval($data["date_record"]),
            strval($data["service_id"]),
            strval($data["client_id"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"Record created",
            "id"=>$connect->lastInsertId()
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Record not created"
        ];
        echo json_encode($responce);
    }
}

function createConsumption($connect, $data){
    
    try{
        $create=$connect->prepare(
        "insert into Consumption (Date_Consumption, Value ) VALUES 
        (?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $create->execute(array(
            strval($data["date_consumption"]),
            strval($data["value"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"Consumption created",
            "id"=>$connect->lastInsertId()
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Consumption not created"
        ];
        echo json_encode($responce);
    }
}

function createIncome($connect, $data){
    
    try{
        $create=$connect->prepare(
        "insert into Income (Date_Income, Value ) VALUES 
        (?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $create->execute(array(
            strval($data["date_income"]),
            strval($data["value"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"Income created",
            "id"=>$connect->lastInsertId()
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Income not created"
        ];
        echo json_encode($responce);
    }
}


function createSupply($connect, $data){
    
    try{
        $create=$connect->prepare(
        "insert into Supply (Date_Supply, Value, Provider_ID  ) VALUES 
        (?,?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $create->execute(array(
            strval($data["date_supply"]),
            strval($data["value"]),
            strval($data["provider_id"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"Supply created",
            "id"=>$connect->lastInsertId()
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Supply not created"
        ];
        echo json_encode($responce);
    }
}


function createSickLeave($connect, $data){
    
    try{
        $create=$connect->prepare(
        "insert into SickLeave (Date_Begin, Date_End, Employe_ID ) VALUES 
        (?,?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $create->execute(array(
            strval($data["date_begin"]),
            strval($data["date_end"]),
            strval($data["employe_id"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"SickLeave created",
            "id"=>$connect->lastInsertId()
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"SickLeave not created"
        ];
        echo json_encode($responce);
    }
}

function createVacation($connect, $data){
    

    //print_r($data); die();
    try{
        $create=$connect->prepare(
        "insert into Vacation (Date_Begin, Date_End, Employe_ID ) VALUES 
        (?,?,?)"
        );
        //var_dump($data);
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $create->execute(array(
            strval($data["date_begin"]),
            strval($data["date_end"]),
            strval($data["employe_id"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"Vacation created",
            "id"=>$connect->lastInsertId()

        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Vacation not created"
        ];
        echo json_encode($responce);
    }
}

function createMaterial($connect, $data){
    

    //print_r($data); die();
    try{
        $create=$connect->prepare(
            "insert into Material (Name_Material, Stock_ID , Supply_ID  ) VALUES 
            (?,?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $create->execute(array(
            strval($data["name_material"]),
            strval($data["stock_id"]),
            strval($data["supply_id"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"Material created"
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"Material not created"
        ];
        echo json_encode($responce);
    }
}

function createTaxReport($connect, $data){
    

    //print_r($data); die();
    try{
        $create=$connect->prepare(
            "insert into TaxReport (Date_Report, Date_Begin, Date_End, Value_Sells,  Value_Tax, Employe_ID ) VALUES 
            (?,?,?,?,?,?)"
        );
        
        //if(empty($data["middlename"])) $data["middlename"]="-";
        $create->execute(array(
            strval($data["date_report"]),
            strval($data["date_begin"]),
            strval($data["date_end"]),
            strval($data["value_sells"]),
            strval($data["value_tax"]),
            strval($data["employe_id"])
        ));
        $responce=[
            "status"=>true,
            "message"=>"TaxReport created"
        ];
        echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"TaxReport not created"
        ];
        echo json_encode($responce);
    }
}



function createEmployee ($connect, $data){

    //var_dump($data); die();
    // print_r( $data); die();
     try{
         $selectUsers=$connect->prepare("Select * from Employe where Email=? or INN=?");
         $selectUsers->execute(array(strval($data["email"]), $data["inn"]));
         if(count($selectUsers->fetchAll())>0){
             $responce=[
                 "status"=>false,
                 "message"=>"user not created"
             ];
             echo json_encode($responce);
             die();
         }



         $createEmploye=$connect->prepare(
         "insert into Employe( FirstName, LastName, MiddleName, Email, Password, INN, Post_ID, Status_ID) VALUES 
         (?,?,?,?,?,?,?,?)"
         );
         
         //if(empty($data["middlename"])) $data["middlename"]="-";
         $createEmploye->execute(array(
             strval($data["firstname"]), 
             strval($data["lastname"]), 
             strval($data["middlename"]), 
             strval($data["email"]),  
             md5(strval($data["password"])), 
             strval($data["inn"]),
             $data["post_id"],
             $data["status_id"]
         ));
         //print_r($data); die();
         $responce=[
             "status"=>true,
             "message"=>"user created"
         ];
         echo json_encode($responce);
         //die();
         
        // exit();
         //$addedEmploye=$createEmploye->fetchAll();
     } catch (PDOException $e) {
          $responce=[
             "status"=>false,
             "message"=>"user not created"
         ];
         echo json_encode($responce);
     }

    }
 
 


